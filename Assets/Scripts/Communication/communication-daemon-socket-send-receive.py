import asyncio
import socket
import time
import janus
import threading

from bleak import BleakClient
from bleak import BleakScanner

CHARACTERISTIC_UUID = "beb5483e-36e1-4688-b7f5-ea07361b26a8"
SERVICE_UUID = "4fafc201-1fb5-459e-8fcc-c5c9c331914b"
SOCKET_PORT = 8000


def initialize_socket():
    unity_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    unity_socket.connect(('127.0.0.1', SOCKET_PORT))
    unity_socket.send(bytes("Hello from python", 'utf-8'))

    return unity_socket

def connect_to_unity(queue2Send, queueRcv):
    unity_socket = initialize_socket()
    print("Connect to Unity")

    while True: # valider que ca l'existe
        if not queueRcv.empty():
            msg = queueRcv.get()
            print(f"Sending: {msg}")
            unity_socket.send(bytes(msg, 'utf-8'))

        # time.sleep(0.05)

async def find_sleeve():
    adress = None
    while adress is None:
        devices = await BleakScanner.discover()
        for d in devices:
            # TODO: receive the name of the device from Unity (maybe send all the names to unity first and let the player select his device?)
            if d.name == "MyESP32":
                print("Found MyESP32")
                adress = d.address
                break
    
    return adress

async def connect_to_sleeve(queue2Send, queueRcv):
    print("Connect to sleeve")
    device = await find_sleeve()
    
    if device is not None:
        async with BleakClient(device) as client:
            print("Connecting to MyESP32")

            while client.is_connected:
                message = await client.read_gatt_char(CHARACTERISTIC_UUID)
                await queueRcv.put(message)
                print(str(message, 'utf-8'))
                
                if not queue2Send.empty():
                    msg = await queue2Send.get()
                    print(f"Sending: {msg}")
                    await client.write_gatt_char(CHARACTERISTIC_UUID, msg.encode('utf-8'))

                time.sleep(0.05)

            print("Client disconnected")


    # return peripheral

async def add_to_queue(queue, msg):
    await queue.put(msg)

async def main():

    queueRcv = janus.Queue()
    queue2Send = janus.Queue()

    await add_to_queue(queue2Send.async_q, "Hello from python")
    await add_to_queue(queue2Send.async_q, "Hello from python again")

    sleeve_connection_task =  asyncio.create_task(connect_to_sleeve(queue2Send.async_q, queueRcv.async_q))
    threading.Thread(target=connect_to_unity, args=(queue2Send.sync_q, queueRcv.sync_q)).start()

    await sleeve_connection_task

asyncio.run(main())

# probleme en ce moment : je recoie les messages de la sleeve, je suis capable d'envoyer des messages a la sleeve, mais je ne suis pas capable d'envoyer les messages de la queue de janus


# loop = asyncio.get_event_loop()
# queueRcv = janus.Queue()
# queue2Send = janus.Queue()

# asyncio.run(add_to_queue(queue2Send, "Hello from python"))
# asyncio.run(add_to_queue(queue2Send, "Hello from python again"))

# asyncio.create_task(connect_to_sleeve(queue2Send.sync_q, queueRcv.sync_q))
# threading.Thread(target=connect_to_unity, args=(queue2Send.sync_q, queueRcv.sync_q)).start()
# loop.run_forever()

# je suis rendue : utiliser la queue de janus a la place de asyncio.Queue (c'est thread safe)
# https://stackoverflow.com/questions/32889527/is-there-a-way-to-use-asyncio-queue-in-multiple-threads
# https://github.com/aio-libs/janus
# explorer aussi https://stackoverflow.com/questions/55027940/is-run-in-executor-optimized-for-running-in-a-loop-with-coroutines

# unity_socket = initialize_socket()
# asyncio.run(connect_to_sleeve(queue2Send, queueRcv))
# send_data(sleeve, unity_socket)

# unity_socket.close()

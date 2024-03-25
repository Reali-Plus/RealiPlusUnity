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

def connect_to_unity(queue_write, queue_read):
    unity_socket = initialize_socket()
    print("Connect to Unity")
    while True: 
        # print(f"UNITY - Queue size: {queue_read.qsize()}")
        if not queue_read.empty():
            msg = queue_read.get()
            # print(msg)
            print(f"Sending: {msg}")
            unity_socket.send(msg)
            data = unity_socket.recv(1024)
            # print(f"Received from unity: {data}")
            queue_write.put(data)

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

async def connect_to_sleeve(queue_write, queue_read):
    print("Connect to sleeve")
    device = await find_sleeve()
    
    if device is not None:
        async with BleakClient(device) as client:
            print("Connecting to MyESP32")

            while client.is_connected:
                message = await client.read_gatt_char(CHARACTERISTIC_UUID)
                await queue_read.put(message)
                # print(str(message, 'utf-8'))
                # print(f"SLEEVE - Queue size: {queue_read.qsize()}")
                
                if not queue_write.empty():
                    msg = await queue_write.get()
                    # print(f"Sending to sleeve: {msg}")
                    await client.write_gatt_char(CHARACTERISTIC_UUID, msg)

                time.sleep(0.05)

            print("Client disconnected")


async def add_to_queue(queue, msg):
    await queue.put(msg)

async def main():

    queue_read = janus.Queue()
    queue_write = janus.Queue()

    await add_to_queue(queue_write.async_q, bytes("Hello from python", 'utf-8'))
    await add_to_queue(queue_write.async_q, bytes("Hello from python again", 'utf-8'))

    sleeve_connection_task = asyncio.create_task(connect_to_sleeve(queue_write.async_q, queue_read.async_q))
    thread = threading.Thread(target=connect_to_unity, args=(queue_write.sync_q, queue_read.sync_q))
    thread.start()

    await sleeve_connection_task

asyncio.run(main())

# probleme en ce moment : je recoie les messages de la sleeve, je suis capable d'envoyer des messages a la sleeve, mais je ne suis pas capable d'envoyer les messages de la queue de janus
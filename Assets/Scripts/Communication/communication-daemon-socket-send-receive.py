import asyncio
import socket
import numpy as np
import time

from bleak import BleakClient
from bleak import BleakScanner

CHARACTERISTIC_UUID = "beb5483e-36e1-4688-b7f5-ea07361b26a8"
SERVICE_UUID = "4fafc201-1fb5-459e-8fcc-c5c9c331914b"
SOCKET_PORT = 8000


def initialize_socket():
    unity_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    unity_socket.connect(('127.0.0.1', SOCKET_PORT))

    return unity_socket

async def connect_to_sleeve(queue2Send : asyncio.Queue, queueRcv : asyncio.Queue):
    print("Connect to sleeve")
    devices = await BleakScanner.discover()
    adress = None
    for d in devices:
        if d.name == "MyESP32":
            print("Found MyESP32")
            adress = d.address
            break
    
    if adress is not None:
        async with BleakClient(adress) as client:
            print("Connecting to MyESP32")
            # await client.connect()
            # print("Connected to MyESP32")

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

# def send_data(sleeve, s):
#     while sleeve.is_connected():
#         message = str(sleeve.read(SERVICE_UUID, CHARACTERISTIC_UUID), 'UTF-8')
#         print(message)
#         s.send(bytes(message, 'utf-8'))
#         time.sleep(0.05)

queueRcv = asyncio.Queue()
queue2Send = asyncio.Queue()
asyncio.run(add_to_queue(queue2Send, "Hello from python"))
asyncio.run(add_to_queue(queue2Send, "Hello from python again"))


# unity_socket = initialize_socket()
asyncio.run(connect_to_sleeve(queue2Send, queueRcv))
# send_data(sleeve, unity_socket)

# unity_socket.close()

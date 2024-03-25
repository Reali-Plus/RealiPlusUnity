import asyncio
import socket
import janus
import threading

from bleak import BleakClient
from bleak import BleakScanner


CHARACTERISTIC_UUID = "beb5483e-36e1-4688-b7f5-ea07361b26a8"
SERVICE_UUID = "4fafc201-1fb5-459e-8fcc-c5c9c331914b"
SOCKET_PORT = 8000


def initialize_socket():
    unity_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    unity_socket.setblocking(0) # non-blocking
    unity_socket.connect(('127.0.0.1', SOCKET_PORT))
    unity_socket.send(bytes("Hello from python", 'utf-8'))

    return unity_socket


def send_to_unity(unity_socket, msg):
    print(f"Sending to unity: {msg}")
    try:
        unity_socket.send(msg)
    except ConnectionResetError:
        print("Connection reset by peer. Reconnecting...")
        unity_socket = initialize_socket()  # Reconnect
        pass
    except BlockingIOError:
        pass


def receive_from_unity(unity_socket):
    try:
        data = unity_socket.recv(1024)
        return data
    except ConnectionResetError:
        print("Connection reset by peer. Reconnecting...")
        unity_socket = initialize_socket()  # Reconnect
        return None
    except BlockingIOError:
        return None


def connect_to_unity(queue_write, queue_read):
    unity_socket = initialize_socket()
    print("Connect to Unity")
    while True: 
        if not queue_read.empty():
            msg = queue_read.get()
            send_to_unity(unity_socket, msg)

            data = receive_from_unity(unity_socket)
            if data is not None:
                queue_write.put(data)


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
                
                if not queue_write.empty():
                    msg = await queue_write.get()
                    # print(f"Sending to sleeve: {msg}")
                    await client.write_gatt_char(CHARACTERISTIC_UUID, msg)

            print("Client disconnected")


async def main():

    queue_read = janus.Queue()
    queue_write = janus.Queue()

    sleeve_connection_task = asyncio.create_task(connect_to_sleeve(queue_write.async_q, queue_read.async_q))
    thread = threading.Thread(target=connect_to_unity, args=(queue_write.sync_q, queue_read.sync_q))
    thread.start()

    await sleeve_connection_task

asyncio.run(main())

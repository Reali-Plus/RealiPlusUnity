import asyncio
import threading
import numpy as np

from bleak import BleakClient
from bleak import BleakScanner

CHARACTERISTIC_UUID = "beb5483e-36e1-4688-b7f5-ea07361b26a8"


class CommunicationDaemon:

    def __init__(self) -> None:
        self.last_message = ""
        self.send_buffer = ['test1', 'test2', 'test3']  # list of messages to send


    async def receive_message(self, client):
        while True:
            message = await client.read_gatt_char(CHARACTERISTIC_UUID)
            # print("Model Number: {0}".format("".join(map(chr, message))))
            message = str(message, 'UTF-8')
            print(f"Message received: {message}")

            if message != self.last_message:
                self.last_message = message
                # write message to file
                try:
                    with open('message.txt', 'w') as file:
                        file.write(message)
                except:
                    print('Could not write to file')
                # with open('message.txt', 'w') as file:
                #     file.write(message)

            # print()
            print(f"Message: {message}")


    async def send_message(self, client):
        while True:
            message = 'test1234'
            await client.write_gatt_char(CHARACTERISTIC_UUID, message.encode('UTF-8'))


    async def connect_to_sleeve(self):
        devices = await BleakScanner.discover()
        address = "test"
        for d in devices:
            if d.name == "MyESP32":
                address = d.address
        
        async with BleakClient(address) as client:
            await client.is_connected()

            print('CONNECTED')
            receive_task = asyncio.create_task(self.receive_message(client))
            # send_task = asyncio.create_task(self.send_message(client))

            await receive_task
            # await send_task
            # t1 = threading.Thread(target=self.receive_message, args=(client,))
            # # t2 = threading.Thread(target=self.send_message, args=(client,))

            # t1.start()
            # # t2.start()

            # t1.join()
            # t2.join()



# comm = CommunicationDaemon()
# asyncio.run(comm.connect_to_sleeve())
i = -2*np.pi    
while True:
    i += 0.1
    if i > 2*np.pi:
        i = -2*np.pi
        
    print(f'-1.07 {i} -0.91')



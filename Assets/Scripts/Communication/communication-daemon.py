import asyncio
from bleak import BleakClient
from bleak import BleakScanner

CHARACTERISTIC_UUID = "beb5483e-36e1-4688-b7f5-ea07361b26a8"


class CommunicationDaemon:

    def __init__(self) -> None:
        self.last_message = ""

    async def connect_to_sleeve(self):
        devices = await BleakScanner.discover()
        address = "test"
        for d in devices:
            if d.name == "MyESP32":
                address = d.address
        
        async with BleakClient(address) as client:
            await client.is_connected()
            print('CONNECTED')

            while True:
                message = await client.read_gatt_char(CHARACTERISTIC_UUID)
                message = str(message, 'UTF-8')

                if message != self.last_message:
                    self.last_message = message
                    # write message to file
                    with open('message.txt', 'w') as file:
                        file.write(message)

                print()
                print(f"Message: {message}")

comm = CommunicationDaemon()

asyncio.run(comm.connect_to_sleeve())

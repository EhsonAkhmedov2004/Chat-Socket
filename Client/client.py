from threading import Thread
import socket       
import rich
from rich.prompt import Prompt
from rich.console import Console



console = Console()




   
 
# Create a socket object
s = socket.socket()        
 
# Define the port on which you want to connect
port = 8000              
 
# connect to the server on local computer
s.connect(('127.0.0.1', port))

 
online = True

def GetMessages():

	while online:
		try:
			response = s.recv(1024).decode()

			# if response['status'] == 0:
			# 	console.log(f"[bold #1E90FF on red] No one is online in the chat now [bold #1E90FF on white](instead of you :)) ",justify="center")
			# 	continue
			#console.log(response['message'])
			if "http" in response:
				console.log(f"[bold #1E90FF underline on red]{response}",justify="right")

			console.log(f"[bold #1E90FF on red]{response}",justify="right")
		except:
			console.print("( ~ Admin ) [italic #1E90FF]Good bye :)")	
			break






def SendMessages():
	console.log("[bold red]( ~ Admin ) Hello User to the [bold #1E90FF] chat terminal application ",style="bold #1E90FF blink")
	name = console.input(" [bold]Please enter your [green]name [red]~ ")
	console.log("[bold red]( ~Admin ) [bold #1E90FF]Good Chat and have a good day ...")


	while online:

		userMessage = console.input()
		console.log(f"[bold #1E90FF on white]{userMessage}")

		if userMessage == "QUIT":
			
			s.close()
			break


		s.send(bytes(f"{userMessage} ",'utf-8'))




def MoveApplication():
	thread = Thread(target=SendMessages)
	thread2 = Thread(target=GetMessages)
	thread.start()
	thread2.start()


MoveApplication()



# # receive data from the server and decoding to get the string.
# print (s.recv(1024).decode())
# # close the connection
# s.close()

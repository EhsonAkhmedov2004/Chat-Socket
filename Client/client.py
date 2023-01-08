from threading import Thread
import asyncio


# def B():
# 	while True:
# 		print("B")




# def A():
# 	count = 0
# 	t = Thread(target=B)
# 	t.start()
# 	while True:
		
# 		print("A");

# t = Thread(target=A)
# t.start()
# print("asd")
# Import socket module





import socket       


class UserIdentification:
	def __init__(self,name):
		self.name = name     
 
# Create a socket object
s = socket.socket()        
 
# Define the port on which you want to connect
port = 8000              
 
# connect to the server on local computer
s.connect(('127.0.0.1', port))
 
online = True

def GetMessages():

	while online:
		print(f"{s.recv(1024).decode()}")





def SendMessages():
	print("--- Hello User to the chat terminal application --- ( ~ Admin )")
	name = input(" Please enter your name : ")
	UI = UserIdentification(name)
	print("( ~ Admin ) Good Chat and have a good day ...")


	while online:
		userMessage = input()
		s.send(bytes(f"{UI.name} : {userMessage} ",'utf-8'))




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

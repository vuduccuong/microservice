import pika
import json

from .models import Product

params = pika.URLParameters('amqp://guest:guest@localhost:49154/')

connection = pika.BlockingConnection(parameters=params)

channel = connection.channel()


def publish(method: str, data: Product):
    property = pika.BasicProperties(method)

    channel.basic_publish(exchange='', routing_key='create_product',
                          body=json.dumps(data), properties=property)

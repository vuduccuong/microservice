import pika, json, os, django

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'config.settings')
django.setup()

from apps.products.models import Product

QUEUE_NAME = 'like_products'

params = pika.URLParameters('amqp://guest:guest@localhost:49154/')

connection = pika.BlockingConnection(parameters=params)

channel = connection.channel()

channel.queue_declare(queue=QUEUE_NAME)

def callback(ch, method, properties, body):
    print(ch)
    print(method)
    print(properties)

    data = json.loads(body)
    id = data.get("Id", None)
    if id:
        product = Product.objects.get(pk=id)
        if product:
            product.likes = product.likes + 1
            product.save()

channel.basic_consume(queue=QUEUE_NAME, on_message_callback=callback, auto_ack=True)

print('start consuming')

channel.start_consuming()

channel.close()
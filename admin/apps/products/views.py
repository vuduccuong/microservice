from rest_framework import viewsets, status
from rest_framework.response import Response

from drf_yasg.utils import swagger_auto_schema
from drf_yasg import openapi

from .serializer import ProductSerializer
from .models import Product
from .producer import publish


class ProductViewSet(viewsets.ViewSet):
    lookup_field = 'pk'

    def list(self, request):
        products = Product.objects.all()
        serializer = ProductSerializer(products, many=True)

        return Response(serializer.data, status=status.HTTP_200_OK)

    @swagger_auto_schema(operation_description="POST /products",
                         request_body=openapi.Schema(
                             type=openapi.TYPE_OBJECT,
                             properties={
                                 'title': openapi.Schema(type=openapi.TYPE_STRING, description='Title'),
                                 'image': openapi.Schema(type=openapi.TYPE_STRING, description='Image'),
                                 'likes': openapi.Schema(type=openapi.TYPE_INTEGER, description='Likes'),
                             }
                         ),
                         )
    def create(self, request, *args, **kwargs):
        data = request.data
        serializer = ProductSerializer(data=data)

        serializer.is_valid(raise_exception=True)
        serializer.save()

        publish('create', serializer.data)

        return Response(serializer.data, status=status.HTTP_201_CREATED)

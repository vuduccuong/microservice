from django.urls import include, path, re_path
from rest_framework import routers

from .views import ProductViewSet
from drf_yasg.views import get_schema_view
from drf_yasg import openapi

schema_view = get_schema_view(
    openapi.Info(
        title="Snippets API",
        default_version='v1',
        terms_of_service="https://www.google.com/policies/terms/",
        contact=openapi.Contact(email="vuduccuong.ck48@gmail.com"),
        license=openapi.License(name="BSD License"),
    ),
    public=True,
)

router = routers.SimpleRouter()

router.register("products", ProductViewSet, basename="products")

urlpatterns = [
    re_path(r'^', include(router.urls)),
    re_path(r'^swagger(?P<format>\.json|\.yaml)$',
            schema_view.without_ui(cache_timeout=0), name='schema-json'),
    re_path(r'^swagger/$', schema_view.with_ui('swagger',
            cache_timeout=0), name='schema-swagger-ui'),
    re_path(r'^redoc/$', schema_view.with_ui('redoc',
            cache_timeout=0), name='schema-redoc'),
]

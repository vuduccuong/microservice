FROM python:3.9

ENV PYTHONUNBUFFERED 1

WORKDIR /container_admin

COPY ./requirements/base.txt /container_admin/requirements.txt

RUN pip install -r requirements.txt

COPY . /container_admin

CMD python manage.py runserver 127.0.0.1:8000
FROM squidfunk/mkdocs-material

COPY ./certs /usr/local/share/ca-certificates/

RUN apk update && \
    apk add --no-cache ca-certificates && \
    update-ca-certificates && \
	pip install --root-user-action=ignore neoteroi-mkdocs mkdocs-awesome-nav
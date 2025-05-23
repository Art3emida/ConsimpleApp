up:
	docker-compose up -d

down:
	docker-compose down

clean: down
	docker-compose rm -f
	docker volume prune -f
	docker network prune -f
	docker image prune -af
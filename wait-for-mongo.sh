#!/bin/bash

# Wait for MongoDB to be ready
echo "Waiting for MongoDB to be ready..."
until mongosh --host mongodb --port 27017 --username root --password example --eval "db.adminCommand('ping')" > /dev/null 2>&1; do
  echo "MongoDB is not ready yet. Waiting..."
  sleep 2
done

echo "MongoDB is ready! Starting application..."
exec "$@"

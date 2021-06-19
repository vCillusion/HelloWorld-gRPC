!/bin/bash
docker build -f Dockerfile -t=reglocal:5000/message-service:v1 ..
echo "Push Message Service Server"
docker push reglocal:5000/message-service:v1

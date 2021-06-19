!/bin/bash
docker build -f Dockerfile -t=reglocal:5000/message-service-client:v1 ..
echo "Push Message Service"
docker push reglocal:5000/message-service-client:v1
!/bin/bash
docker build -f Dockerfile -t=reglocal:5000/hello-world:v1 ..
echo "Push Hello World Server"
docker push reglocal:5000/hello-world:v1

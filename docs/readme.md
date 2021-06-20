# installation steps

## add shared network drive in virtual box or clone the repository directly in the VM
use below command to grant access to the media directory
`sudo adduser $USER vboxsf`

## project build order
each project has a docker batch file to create a docker image
MessageService
MessageServiceClient (can connect from the host using 192.168.5.21:30531)
HelloWorld
Client (can connect from host using 192.168.5.21:30530)



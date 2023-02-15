#!/bin/zsh
sudo ifconfig en0 down
sleep 2
sudo ifconfig en0 up
echo "Network adapter reboot complete!"
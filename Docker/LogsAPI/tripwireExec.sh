#!/bin/bash
no| apt-get -y install tripwire
cat SettingTripWire.txt >> /etc/tripwire/twpol.txt
twadmin --create-polfile -s -Q "" -S /etc/tripwire/site.key /etc/tripwire/twpol.txt && tripwire --init -P ""

cd /app
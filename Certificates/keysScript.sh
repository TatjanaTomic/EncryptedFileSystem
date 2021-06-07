openssl genrsa -out Certificates/private/$1_private.key 2>> Certificates/keyslog.txt

openssl rsa -in Certificates/private/$1_private.key -out Certificates/public/$1_public.key -pubout 2>> Certificates/keyslog.txt

echo "##############################################" >> Certificates/keyslog.txt
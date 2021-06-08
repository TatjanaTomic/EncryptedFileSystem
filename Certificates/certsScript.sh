openssl req -new -key Certificates/private/$1_private.key -config Certificates/openssl.cnf -out Certificates/requests/$1.csr -verbose -batch 2>> Certificates/certslog.txt

openssl ca -in Certificates/requests/$1.csr -out Certificates/certs/$1.crt -config Certificates/openssl.cnf -key tatjana -batch 2>> Certificates/certslog.txt

openssl ca -gencrl -out Certificates/crl/crllist.crl -config Certificates/openssl.cnf -key tatjana 2>> Certificates/certslog.txt

echo "#######################################################" >> Certificates/certslog.txt
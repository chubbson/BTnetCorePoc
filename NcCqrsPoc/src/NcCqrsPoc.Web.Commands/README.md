# Postman, test commands

### Subsidiary

-

Initial create commant of non existent subsidiary  
POST: http://localhost:29169/api/subsidiary/create  
Header: Content-Type application/json  
```
{"SubsidiaryID":"1","StreetAddress":"Teststrasse 1","City":"Testhause","PostalCode":"1234"}
```
Status: 200

-

Run same POST with existant SubsidiaryID 1, should return an error cuz validation fails  
POST: http://localhost:29169/api/subsidiary/create  
Header: Content-Type application/json  
```
{"SubsidiaryID":"1","StreetAddress":"Teststrasse 1","City":"Testhause","PostalCode":"1234"}
```
Status: 400 Bad Request  
Result  
```
{
  "message": "The request is invalid.",
  "errors": {
    "SubsidiaryID": [
      "A Subsidiary with this ID already exists."
    ]
  }
}
```

-

Add a new Subsidiary POST  
POST: http://localhost:29169/api/subsidiary/create  
Header: Content-Type application/json  
```
{"SubsidiaryID":"2","StreetAddress":"somestreet","City":"anywhere","PostalCode":"1234"}
```
Status: 200  

-

Check added subsidiaries: GET  
GET: http://localhost:16144/api/subsidiaries/all  
Status: 200 Ok  
Result  
```
[
  {
    "subsidiaryID": 1,
    "streetAddress": "Teststrasse 1",
    "city": "Testhause",
    "postalCode": "1234",
    "employees": [],
    "aggregateID": "217013d9-3460-4ccc-a117-ed16d29b3db7"
  },
  {
    "subsidiaryID": 2,
    "streetAddress": "somestreet",
    "city": "anywhere",
    "postalCode": "1234",
    "employees": [],
    "aggregateID": "22440a6f-96a9-4d6a-91e0-4f10d982cc96"
  }
]
```

-

Check explicity added subsidiary with ID 2: GET  
GET: http://localhost:16144/api/subsidiaries/2  
Status: 200 Ok  
Result  
```
{
  "subsidiaryID": 2,
  "streetAddress": "somestreet",
  "city": "anywhere",
  "postalCode": "1234",
  "employees": [],
  "aggregateID": "22440a6f-96a9-4d6a-91e0-4f10d982cc96"
}
```

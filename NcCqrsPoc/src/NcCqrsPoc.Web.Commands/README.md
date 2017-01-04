# Postman, test commands

## Subsidiary

## POST Commands

Initial create commant of non existent subsidiary
POST: http://localhost:29169/api/subsidiary/create
Header: Content-Type application/json
```
{"SubsidiaryID":"1","StreetAddress":"Teststrasse 1","City":"Testhause","PostalCode":"1234"}
```
Status: 200

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

# API Documentation

This project fetch a list of products from an API URL that can be configured in the appsettings.json file

You must have dotnet 6 SDK installed before you can run the app

## Build the project

    dotnet build .

## Run the app

    dotnet run --project .\Products.Api\Products.Api.csproj

## Run the tests

    dotnet test .

# REST API

The API to get the list of products is described below.

## Get list of all Products

### Request

`GET /api/v1/products/filter`

    curl -i -H 'Accept: application/json' http://localhost:5005/api/v1/products/filter

### Response

{
    "status": 200,
    "message": "Successful",
    "products": [
        {
            "title": "A Red Trouser",
            "price": 10,
            "sizes": ["small", "medium", "large"],
            "description": "This trouser perfectly pairs with a green shirt."
        },
        {
            "title": "A Green Trouser",
            "price": 11,
            "sizes": ["small"],
            "description": "This trouser perfectly pairs with a blue shirt."
        },
        ...
    ],
    "filter": {
        "minPrice": 10,
        "maxPrice": 25,
        "sizes": ["small", "medium", "large"],
        "commonWords": [
            "hat", "trouser","green", "blue", "red", "belt", "bag", "shoe", "tie"
        ]
    }
}


## Get list of Products with filter

### Request

`GET /api/v1/products/filter?maxprice=20&size=medium&highlight=green,blue `

    curl -i -H 'Accept: application/json' http://localhost:5005/api/v1/products/filter?maxprice=20&size=medium&highlight=green,blue 

### Response

{
    "status": 200,
    "message": "Successful",
    "products": [
        {
            "title": "A Red Trouser",
            "price": 10,
            "sizes": ["small", "medium", "large"],
            "description": "This trouser perfectly pairs with a green shirt."
        },
        {
            "title": "A Green Trouser",
            "price": 11,
            "sizes": ["small"],
            "description": "This trouser perfectly pairs with a blue shirt."
        },
        ...
    ],
    "filter": {
        "minPrice": 10,
        "maxPrice": 25,
        "sizes": ["small", "medium", "large"],
        "commonWords": [
            "hat", "trouser","green", "blue", "red", "belt", "bag", "shoe", "tie"
        ]
    }
}



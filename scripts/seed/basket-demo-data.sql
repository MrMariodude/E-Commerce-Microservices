BEGIN;

INSERT INTO public.mt_doc_shoppingcart (id, data, mt_dotnet_type)
VALUES
(
    'omar',
    $${
      "UserName": "omar",
      "Items": [
        {
          "Quantity": 1,
          "Color": "graphite",
          "Price": 109.99,
          "ProductID": "11111111-1111-1111-1111-111111111111",
          "ProductName": "Logitech MX Keys S Keyboard"
        },
        {
          "Quantity": 1,
          "Color": "black",
          "Price": 99.99,
          "ProductID": "22222222-2222-2222-2222-222222222222",
          "ProductName": "Logitech MX Master 3S Mouse"
        }
      ],
      "TotalPrice": 209.98
    }$$::jsonb,
    'Basket.Models.ShoppingCart'
),
(
    'sara',
    $${
      "UserName": "sara",
      "Items": [
        {
          "Quantity": 1,
          "Color": "silver",
          "Price": 349.99,
          "ProductID": "33333333-3333-3333-3333-333333333333",
          "ProductName": "Dell UltraSharp 27 Monitor"
        }
      ],
      "TotalPrice": 349.99
    }$$::jsonb,
    'Basket.Models.ShoppingCart'
),
(
    'ahmed',
    $${
      "UserName": "ahmed",
      "Items": [
        {
          "Quantity": 1,
          "Color": "space gray",
          "Price": 59.99,
          "ProductID": "44444444-4444-4444-4444-444444444444",
          "ProductName": "Anker 8-in-1 USB-C Hub"
        },
        {
          "Quantity": 1,
          "Color": "black",
          "Price": 179.99,
          "ProductID": "88888888-8888-8888-8888-888888888888",
          "ProductName": "Logitech Brio 4K Webcam"
        }
      ],
      "TotalPrice": 239.98
    }$$::jsonb,
    'Basket.Models.ShoppingCart'
),
(
    'lina',
    $${
      "UserName": "lina",
      "Items": [
        {
          "Quantity": 1,
          "Color": "black",
          "Price": 399.99,
          "ProductID": "55555555-5555-5555-5555-555555555555",
          "ProductName": "Sony WH-1000XM5 Headphones"
        }
      ],
      "TotalPrice": 399.99
    }$$::jsonb,
    'Basket.Models.ShoppingCart'
),
(
    'mohamed',
    $${
      "UserName": "mohamed",
      "Items": [
        {
          "Quantity": 1,
          "Color": "bamboo",
          "Price": 699.99,
          "ProductID": "66666666-6666-6666-6666-666666666666",
          "ProductName": "Fully Jarvis Standing Desk"
        }
      ],
      "TotalPrice": 699.99
    }$$::jsonb,
    'Basket.Models.ShoppingCart'
),
(
    'nora',
    $${
      "UserName": "nora",
      "Items": [
        {
          "Quantity": 1,
          "Color": "graphite",
          "Price": 1499.00,
          "ProductID": "77777777-7777-7777-7777-777777777777",
          "ProductName": "Herman Miller Aeron Chair"
        }
      ],
      "TotalPrice": 1499.00
    }$$::jsonb,
    'Basket.Models.ShoppingCart'
),
(
    'youssef',
    $${
      "UserName": "youssef",
      "Items": [
        {
          "Quantity": 1,
          "Color": "silver",
          "Price": 49.99,
          "ProductID": "99999999-9999-9999-9999-999999999999",
          "ProductName": "Rain Design mStand Laptop Stand"
        },
        {
          "Quantity": 1,
          "Color": "black",
          "Price": 189.99,
          "ProductID": "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
          "ProductName": "Samsung T9 Portable SSD 2TB"
        }
      ],
      "TotalPrice": 239.98
    }$$::jsonb,
    'Basket.Models.ShoppingCart'
),
(
    'fatma',
    $${
      "UserName": "fatma",
      "Items": [
        {
          "Quantity": 2,
          "Color": "graphite",
          "Price": 109.99,
          "ProductID": "11111111-1111-1111-1111-111111111111",
          "ProductName": "Logitech MX Keys S Keyboard"
        }
      ],
      "TotalPrice": 219.98
    }$$::jsonb,
    'Basket.Models.ShoppingCart'
),
(
    'karim',
    $${
      "UserName": "karim",
      "Items": [
        {
          "Quantity": 1,
          "Color": "black",
          "Price": 99.99,
          "ProductID": "22222222-2222-2222-2222-222222222222",
          "ProductName": "Logitech MX Master 3S Mouse"
        },
        {
          "Quantity": 1,
          "Color": "black",
          "Price": 399.99,
          "ProductID": "55555555-5555-5555-5555-555555555555",
          "ProductName": "Sony WH-1000XM5 Headphones"
        }
      ],
      "TotalPrice": 499.98
    }$$::jsonb,
    'Basket.Models.ShoppingCart'
),
(
    'mariam',
    $${
      "UserName": "mariam",
      "Items": [
        {
          "Quantity": 1,
          "Color": "silver",
          "Price": 349.99,
          "ProductID": "33333333-3333-3333-3333-333333333333",
          "ProductName": "Dell UltraSharp 27 Monitor"
        },
        {
          "Quantity": 1,
          "Color": "black",
          "Price": 179.99,
          "ProductID": "88888888-8888-8888-8888-888888888888",
          "ProductName": "Logitech Brio 4K Webcam"
        },
        {
          "Quantity": 1,
          "Color": "space gray",
          "Price": 59.99,
          "ProductID": "44444444-4444-4444-4444-444444444444",
          "ProductName": "Anker 8-in-1 USB-C Hub"
        }
      ],
      "TotalPrice": 589.97
    }$$::jsonb,
    'Basket.Models.ShoppingCart'
)
ON CONFLICT (id) DO UPDATE
SET
    data = EXCLUDED.data,
    mt_dotnet_type = EXCLUDED.mt_dotnet_type;

COMMIT;

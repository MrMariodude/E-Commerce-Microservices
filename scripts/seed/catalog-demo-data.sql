BEGIN;

INSERT INTO public.mt_doc_product (id, data, mt_dotnet_type)
VALUES
(
    '11111111-1111-1111-1111-111111111111',
    $${
      "ID": "11111111-1111-1111-1111-111111111111",
      "Name": "Logitech MX Keys S Keyboard",
      "Categories": ["Electronics", "Computer Accessories"],
      "Description": "Full-size wireless keyboard with smart backlighting and quiet typing.",
      "ImageFile": "logitech-mx-keys-s.jpg",
      "Price": 109.99
    }$$::jsonb,
    'Catalog.API.Models.Product'
),
(
    '22222222-2222-2222-2222-222222222222',
    $${
      "ID": "22222222-2222-2222-2222-222222222222",
      "Name": "Logitech MX Master 3S Mouse",
      "Categories": ["Electronics", "Computer Accessories"],
      "Description": "Ergonomic wireless mouse built for productivity and precision work.",
      "ImageFile": "logitech-mx-master-3s.jpg",
      "Price": 99.99
    }$$::jsonb,
    'Catalog.API.Models.Product'
),
(
    '33333333-3333-3333-3333-333333333333',
    $${
      "ID": "33333333-3333-3333-3333-333333333333",
      "Name": "Dell UltraSharp 27 Monitor",
      "Categories": ["Electronics", "Monitors"],
      "Description": "27-inch QHD monitor with sharp color accuracy for office and creative use.",
      "ImageFile": "dell-ultrasharp-27.jpg",
      "Price": 349.99
    }$$::jsonb,
    'Catalog.API.Models.Product'
),
(
    '44444444-4444-4444-4444-444444444444',
    $${
      "ID": "44444444-4444-4444-4444-444444444444",
      "Name": "Anker 8-in-1 USB-C Hub",
      "Categories": ["Electronics", "Laptop Accessories"],
      "Description": "Compact USB-C hub with HDMI, Ethernet, card reader, and power delivery.",
      "ImageFile": "anker-8-in-1-usbc-hub.jpg",
      "Price": 59.99
    }$$::jsonb,
    'Catalog.API.Models.Product'
),
(
    '55555555-5555-5555-5555-555555555555',
    $${
      "ID": "55555555-5555-5555-5555-555555555555",
      "Name": "Sony WH-1000XM5 Headphones",
      "Categories": ["Electronics", "Audio"],
      "Description": "Premium wireless noise-canceling headphones for work, calls, and travel.",
      "ImageFile": "sony-wh-1000xm5.jpg",
      "Price": 399.99
    }$$::jsonb,
    'Catalog.API.Models.Product'
),
(
    '66666666-6666-6666-6666-666666666666',
    $${
      "ID": "66666666-6666-6666-6666-666666666666",
      "Name": "Fully Jarvis Standing Desk",
      "Categories": ["Office Furniture", "Desks"],
      "Description": "Electric height-adjustable standing desk designed for home office comfort.",
      "ImageFile": "fully-jarvis-standing-desk.jpg",
      "Price": 699.99
    }$$::jsonb,
    'Catalog.API.Models.Product'
),
(
    '77777777-7777-7777-7777-777777777777',
    $${
      "ID": "77777777-7777-7777-7777-777777777777",
      "Name": "Herman Miller Aeron Chair",
      "Categories": ["Office Furniture", "Chairs"],
      "Description": "Iconic ergonomic office chair with breathable mesh support.",
      "ImageFile": "herman-miller-aeron.jpg",
      "Price": 1499.00
    }$$::jsonb,
    'Catalog.API.Models.Product'
),
(
    '88888888-8888-8888-8888-888888888888',
    $${
      "ID": "88888888-8888-8888-8888-888888888888",
      "Name": "Logitech Brio 4K Webcam",
      "Categories": ["Electronics", "Cameras"],
      "Description": "4K webcam with sharp video quality for remote work and meetings.",
      "ImageFile": "logitech-brio-4k.jpg",
      "Price": 179.99
    }$$::jsonb,
    'Catalog.API.Models.Product'
),
(
    '99999999-9999-9999-9999-999999999999',
    $${
      "ID": "99999999-9999-9999-9999-999999999999",
      "Name": "Rain Design mStand Laptop Stand",
      "Categories": ["Laptop Accessories", "Office Setup"],
      "Description": "Aluminum laptop stand that improves posture and desk organization.",
      "ImageFile": "rain-design-mstand.jpg",
      "Price": 49.99
    }$$::jsonb,
    'Catalog.API.Models.Product'
),
(
    'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa',
    $${
      "ID": "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
      "Name": "Samsung T9 Portable SSD 2TB",
      "Categories": ["Electronics", "Storage"],
      "Description": "High-speed portable SSD for backups, projects, and media files.",
      "ImageFile": "samsung-t9-2tb.jpg",
      "Price": 189.99
    }$$::jsonb,
    'Catalog.API.Models.Product'
)
ON CONFLICT (id) DO UPDATE
SET
    data = EXCLUDED.data,
    mt_dotnet_type = EXCLUDED.mt_dotnet_type;

COMMIT;

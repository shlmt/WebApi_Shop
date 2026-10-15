SET XACT_ABORT ON;

IF OBJECT_ID(N'dbo.Categories', N'U') IS NULL
   OR OBJECT_ID(N'dbo.Products', N'U') IS NULL
BEGIN
    THROW 50001, 'Categories and Products tables must exist before running this seed script.', 1;
END;

BEGIN TRANSACTION;

ALTER TABLE dbo.Products
    ALTER COLUMN productName NVARCHAR(MAX) NOT NULL;

INSERT INTO dbo.Categories (categoryName)
SELECT seed.categoryName
FROM (VALUES
    (N'מרקים'),
    (N'קינוחים'),
    (N'דגים'),
    (N'פיצות'),
    (N'גבינות'),
    (N'סלטים'),
    (N'שתייה'),
    (N'פסטות'),
    (N'מאפים'),
    (N'מנות קלות'),
    (N'כריכים')
) AS seed(categoryName)
WHERE NOT EXISTS (
    SELECT 1
    FROM dbo.Categories AS category
    WHERE category.categoryName = seed.categoryName
);

INSERT INTO dbo.Products (productName, price, categoryId, description, imageUrl)
SELECT seed.productName, seed.price, category.categoryId, seed.description, seed.imageUrl
FROM (VALUES
    (N'מרק בטטה', 28, N'מרקים', N'מרק בטטה', N'batataSoup.jpg'),
    (N'עוגת שוקולד', 32, N'קינוחים', N'עוגת שוקולד', N'chocolata.jpg'),
    (N'דג משפחתי', 89, N'דגים', N'ארוחת דג משפחתית', N'familyFish&.jpg'),
    (N'פיצה משפחתית', 65, N'פיצות', N'פיצה משפחתית', N'familyPizza.jpg'),
    (N'דג וצ''יפס', 54, N'דגים', N'דג בציפוי פריך עם צ''יפס', N'fishAndChips.jpg'),
    (N'חלומי', 42, N'גבינות', N'גבינת חלומי חמה', N'HALUMI.jpg'),
    (N'סלט הבית', 38, N'סלטים', N'סלט הבית', N'homeFrizeSalad.jpg'),
    (N'קפה קר', 18, N'שתייה', N'קפה קר', N'iceCoffee.jpg'),
    (N'קרפ', 26, N'קינוחים', N'קרפ מתוק', N'krep.jpg'),
    (N'מרק בצל', 28, N'מרקים', N'מרק בצל', N'onionSoup.jpg'),
    (N'פסטה שמנת', 48, N'פסטות', N'פסטה חלומית', N'pastaDreemy.jpg'),
    (N'פסטה איטריות', 46, N'פסטות', N'פסטה איטריות', N'pastaItriot.JPG'),
    (N'פסטה פטריות', 49, N'פסטות', N'פסטה פטריות', N'pastaMushrooms.jpg'),
    (N'פיצה', 48, N'פיצות', N'פיצה גבינות', N'pizza.jpg'),
    (N'פוקאצ''ה', 34, N'מאפים', N'פוקאצ''ה חציל', N'pocachaEggplant.jpg'),
    (N'סיידר', 17, N'שתייה', N'סיידר תפוחים', N'sayder.jpg'),
    (N'שוקוצ''ינו', 20, N'שתייה', N'שוקוצ''ינו', N'shokochino.jpg'),
    (N'דג קטן', 39, N'דגים', N'דג וצ''יפס אישי', N'smallFish&chips.jpg'),
    (N'טוסט וסלט', 39, N'מנות קלות', N'טוסט עם סלט', N'tost&salad.jpg'),
    (N'טוסט', 29, N'כריכים', N'טוסט גבינה', N'tost.jpg'),
    (N'וופל', 30, N'קינוחים', N'וופל מתוק', N'vaffle.jpg')
) AS seed(productName, price, categoryName, description, imageUrl)
INNER JOIN dbo.Categories AS category
    ON category.categoryName = seed.categoryName
WHERE NOT EXISTS (
    SELECT 1
    FROM dbo.Products AS product
    WHERE product.imageUrl = seed.imageUrl
);

COMMIT TRANSACTION;
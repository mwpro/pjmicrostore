
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (1, N'Laptopy i tablety', NULL)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (2, N'Laptopy', 1)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (3, N'Tablety', 1)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (4, N'Laptopy 2w1', 1)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (5, N'Peryferia', NULL)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (6, N'Monitory', 5)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (7, N'Drukarki', 5)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (8, N'Klawiatury', 5)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (9, N'Gaming', NULL)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (10, N'PC', 9)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (12, N'PS4', 10)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (13, N'XBox', 10)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (14, N'Atramentowe', 7)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId]) VALUES (15, N'Laserowe', 7)
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsDeleted], [IsActive]) VALUES (1, N'Huawei MateBook 13 i7-8565U/8GB/512/MX150/Win10 Dotykowy', N'Odkryj zdumiewające możliwości Huawei MateBook 13 z dotykowym ekranem FullView 2K. Zobacz więcej detali. Odkryj bogatsze kolory. Podziwiaj realistyczne efekty wizualne na wyświetlaczu, który wypełnia aż 88% powierzchni wewnętrznej. Wewnątrz potężny procesor Intel Core i7 8-generacji oraz perfekcyjnie zoptymalizowany BIOS, które uruchamiają laptop w ciągu sekundy. A gdy MateBook 13 rozpocznie pracę, będzie ją kontynuował nawet przez 10 godzin ciągiem. Koniecznie połącz go ze smartfonem Huawei z pomocą OneHop.', CAST(5299.00 AS Decimal(18, 2)), 1, 0, 1)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsDeleted], [IsActive]) VALUES (3, N'Dell Inspiron 3567 i5-7200U/8GB/256/Win10 FHD
', N'15-calowy notebook o wydajności, której potrzebujesz, wyposażony w procesor najwyższej klasy, doskonały wyświetlacz i baterię o długim czasie pracy.

Sprawdź, jak Dell Inspirion 3567 wygląda w rzeczywistości. Chwyć zdjęcie poniżej i przeciągnij je w lewo lub prawo, aby obrócić produkt, lub skorzystaj z przycisków nawigacyjnych.', CAST(2499.00 AS Decimal(18, 2)), 1, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO

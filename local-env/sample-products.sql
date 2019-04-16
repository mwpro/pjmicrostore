-- copied from x-kom.pl
USE [Products.Catalog]
GO
SET IDENTITY_INSERT [dbo].[Attributes] ON 
GO
INSERT [dbo].[Attributes] ([Id], [Name]) VALUES (1, N'CPU')
GO
INSERT [dbo].[Attributes] ([Id], [Name]) VALUES (2, N'RAM')
GO
INSERT [dbo].[Attributes] ([Id], [Name]) VALUES (3, N'SSD')
GO
SET IDENTITY_INSERT [dbo].[Attributes] OFF
GO
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
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (1, N'Huawei MateBook 13 i7-8565U/8GB/512/MX150/Win10 Dotykowy', N'Odkryj zdumiewające możliwości Huawei MateBook 13 z dotykowym ekranem FullView 2K. Zobacz więcej detali. Odkryj bogatsze kolory. Podziwiaj realistyczne efekty wizualne na wyświetlaczu, który wypełnia aż 88% powierzchni wewnętrznej. Wewnątrz potężny procesor Intel Core i7 8-generacji oraz perfekcyjnie zoptymalizowany BIOS, które uruchamiają laptop w ciągu sekundy. A gdy MateBook 13 rozpocznie pracę, będzie ją kontynuował nawet przez 10 godzin ciągiem. Koniecznie połącz go ze smartfonem Huawei z pomocą OneHop.', CAST(5299.00 AS Decimal(18, 2)), 1, 1, 0)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (3, N'Dell Inspiron 3567 i5-7200U/8GB/256/Win10 FHD
', N'15-calowy notebook o wydajności, której potrzebujesz, wyposażony w procesor najwyższej klasy, doskonały wyświetlacz i baterię o długim czasie pracy.

Sprawdź, jak Dell Inspirion 3567 wygląda w rzeczywistości. Chwyć zdjęcie poniżej i przeciągnij je w lewo lub prawo, aby obrócić produkt, lub skorzystaj z przycisków nawigacyjnych.', CAST(2499.00 AS Decimal(18, 2)), 1, 1, 0)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (22, N'MSI GL63 i7-8750H/32GB/1TB GTX1050', N'', CAST(4849.00 AS Decimal(18, 2)), 1, 0, 0)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (23, N'Apple MacBook Air i5/8GB/128GB/HD 6000/Mac OS', N'Z Apple MacBook Air będziesz mógł pracować czy też oglądać filmy z iTunes przez cały dzień, dzięki wydajności baterii gwarantującej 12 godzinny czas pracy na jednym ładowaniu. Wiedząc, że maksymalny stan utrzymania komputera w stanie gotowości to aż do 30 dni, spokojnie możesz wyjechać na parę tygodni a po powrocie kontynuować przerwane zajęcia.', CAST(4099.00 AS Decimal(18, 2)), 1, 0, 0)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (24, N'Apple MacBook Pro i5 2,3GHz/8GB/128/Iris 640 Silver', N'Jest niewiarygodnie smukły, lekki jak piórko, a do tego potężniejszy i szybszy niż kiedykolwiek. Ma najjaśniejszy i najbarwniejszy ekran ze wszystkich notebooków Mac.', CAST(5599.00 AS Decimal(18, 2)), 1, 0, 0)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (25, N'Lenovo Ideapad S130-14 N5000/4GB/128/Win10', N'Zaprojektowana od nowa obudowa o czystych i prostych liniach, które nadają nowoczesny akcent klasycznemu stylowi. Pokryty specjalną powłoką chroniącą przed zniszczeniem. Ultrasmukły Ideapad S130 waży zaledwie 1,47 kg i doskonale się sprawdza w pracy mobilnej.

Sprawdź, jak Lenovo Ideapad S130 wygląda w rzeczywistości. Chwyć zdjęcie poniżej i przeciągnij je w lewo lub prawo, aby obrócić produkt, lub skorzystaj z przycisków nawigacyjnych.', CAST(1599.00 AS Decimal(18, 2)), 1, 0, 0)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (26, N'Lenovo ThinkPad E480 i5-8250U/8GB/512/Win10P', N'Wyróżnij swoją firmę dzięki tym nowocześnie zaprojektowanym, bogato wyposażonym laptopom, które łączą w sobie bezpieczeństwo i produktywność Lenovo Solutions for Small Business. Laptop Lenovo ThinkPad E480 jest wydajny i wytrzymały, a jednocześnie tak lekki, że możesz zabrać go ze sobą wszędzie. Usprawnia pracę dzięki płynnemu działaniu, szybkiemu procesorowi, wyświetlaczowi o wysokiej rozdzielczości oraz ergonomicznej klawiaturze.

Sprawdź, jak ThinkPad E480 wygląda w rzeczywistości. Chwyć zdjęcie poniżej i przeciągnij je w lewo lub prawo aby obrócić produkt lub skorzystaj z przycisków nawigacyjnych.', CAST(3699.00 AS Decimal(18, 2)), 1, 0, 0)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (27, N'ASUS ZenBook Flip UX362FA i7-8565U/16GB/512/W10 Grey', N'ZenBook Flip 13 został wyposażony w nowy, bezramkowy wyświetlacz NanoEdge, który sprawia, że jest prawie o 10% mniejszy niż jego poprzednik. 13-calowy wyświetlacz ma ultra wąskie ramki ze wszystkich czterech stron, co daje 90% stosunek ekranu do obudowy, co pozwala na znacznie bardziej kompaktową konstrukcję. Dzięki ergonomicznemu zawiasowi ErgoLift z możliwością obracania o 360 °, najnowszym procesorom Intel® Core ™, ZenBook Flip 13 jest wszechstronny, wydajny i wyjątkowo przenośny.

Sprawdź, jak produkt wygląda w rzeczywistości. Chwyć zdjęcie poniżej i przeciągnij je w lewo lub prawo aby obrócić produkt lub skorzystaj z przycisków nawigacyjnych.', CAST(5699.00 AS Decimal(18, 2)), 1, 0, 0)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (28, N'MSI GF63 i5-8300H/8GB/240+1TB/Win10X GTX1050', N'MSI GF63 8RC to najnowocześniejszy produkt światowego producenta sprzętu przeznaczonego do zaawansowanego gamingu. GF63 8RC na pierwszy rzut oka odznacza się niesamowitą matrycą IPS z ultracienkimi ramkami co sprawia, ze ekran zajmuje więcej miejsca i prezentuje się niezwykle stylowo. Ponadto uwagę przykuwa odświeżony design laptopa wraz z podświetlaną klawiaturą. We wnętrzu urządzenia znajduje się czterordzeniowy procesor Intel Core ósmej generacji, wydajna karta graficzna GTX 1050, szybka pamięć RAM DDR4 oraz pojemne i szybkie dyski.', CAST(3799.00 AS Decimal(18, 2)), 1, 0, 0)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (29, N'ASUS TUF Gaming FX705GM i7-8750H/16GB/256PCIe+1TB', N'ASUS TUF Gaming FX705 zmieni sposób, w jaki patrzysz na laptopy do gier. Przekracza on oczekiwania, oferując imponujący sprzęt i kompaktową, agresywnie zaprojektowaną konstrukcję, która jest wyjątkowo wytrzymała. Na wydajną pracę przekłada się nowoczesny procesor Intel Core i7 ósmej generacji, karta graficzna GeForce GTX 1060, wydajna pamięć RAM oraz szybka i pojemna pamięć masowa.', CAST(5299.00 AS Decimal(18, 2)), 1, 0, 0)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (30, N'HP 17 i3-7020U/8GB/240/Win10 IPS', N'Zaprojektowany z myślą o długotrwałej wydajności, stylowy laptop HP z ekranem o przekątnej 17,3″ został wyposażony w akumulator o długim czasie pracy, który zapewni łączność, rozrywkę i wydajność przez cały dzień. Zrealizuj błyskawicznie swoje zadania lub usiądź wygodnie i oddaj się rozrywce dzięki procesorowi Intel i bogatym w detale ekranowi Full HD.', CAST(2599.00 AS Decimal(18, 2)), 1, 0, 0)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [IsActive], [IsDeleted]) VALUES (31, N'ASUS ZenBook UX433FN i7-8565U/16GB/512PCIe/Win10', N'ASUS ZenBook UX433FN i7-8565U/16GB/512PCIe/Win10', CAST(5499.00 AS Decimal(18, 2)), 1, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (1, 1, N'i7-8565U')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (1, 2, N'8')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (1, 3, N'512')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (3, 1, N'i5-7200U')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (3, 2, N'8')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (3, 3, N'256')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (22, 1, N'i7-8750H')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (22, 2, N'32')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (22, 3, N'480')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (23, 1, N'i5')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (23, 2, N'8')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (23, 3, N'128')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (24, 1, N'i5')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (24, 2, N'8')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (24, 3, N'128')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (25, 1, N'Pentium N5000')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (25, 2, N'4')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (25, 3, N'128')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (26, 1, N'i5-8250U')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (26, 2, N'8')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (26, 3, N'512')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (27, 1, N'i7-8565U')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (27, 2, N'16')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (27, 3, N'512')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (28, 1, N'i5-8300H')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (28, 2, N'8')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (28, 3, N'240')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (29, 1, N'i7-8750H')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (29, 2, N'16')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (29, 3, N'256')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (30, 1, N'i3-7020U')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (30, 2, N'8')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (30, 3, N'240')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (31, 1, N'i7-8565U')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (31, 2, N'16')
GO
INSERT [dbo].[AttributeValue] ([ProductId], [AttributeId], [Value]) VALUES (31, 3, N'512')
GO

-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Ápr 30. 21:35
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `skateeventdb`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `events`
--

CREATE TABLE `events` (
  `Id` int(11) NOT NULL,
  `StartDate` datetime(6) NOT NULL,
  `EndDate` datetime(6) NOT NULL,
  `Name` longtext NOT NULL,
  `AvailablePLaces` int(11) NOT NULL,
  `Accommodation` int(11) NOT NULL,
  `Reserved` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `events`
--

INSERT INTO `events` (`Id`, `StartDate`, `EndDate`, `Name`, `AvailablePLaces`, `Accommodation`, `Reserved`) VALUES
(1, '2025-04-30 00:00:00.000000', '2025-04-30 00:00:00.000000', 'Skate Ring at Liget', 110, 0, 22),
(2, '2025-06-09 00:00:00.000000', '2025-06-10 00:00:00.000000', 'Rolling Disco', 497, 0, 70),
(3, '2025-07-22 00:00:00.000000', '2025-07-22 00:00:00.000000', 'Skate under the starry sky', 103, 0, 6),
(4, '2025-06-11 10:00:00.000000', '2025-06-12 18:00:00.000000', 'Skate Ring at Liget', 80, 0, 17),
(13, '2025-12-21 11:11:00.000000', '2025-12-21 21:21:00.000000', 'asdfghjklé', 100, 0, 10);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `rent`
--

CREATE TABLE `rent` (
  `Id` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `EventID` int(11) NOT NULL,
  `FeetSize` double NOT NULL,
  `SkateID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `rent`
--

INSERT INTO `rent` (`Id`, `UserID`, `EventID`, `FeetSize`, `SkateID`) VALUES
(19, 3, 2, 38, 23),
(20, 3, 1, 45, 10),
(21, 3, 3, 37, 12),
(27, 4, 1, 38, 2),
(28, 4, 1, 0, 1),
(29, 4, 1, 0, 1),
(30, 4, 1, 0, 1),
(35, 8, 3, 0, 1),
(37, 4, 1, 45, 20),
(38, 9, 2, 44, 19);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `skates`
--

CREATE TABLE `skates` (
  `Id` int(11) NOT NULL,
  `Size` double NOT NULL,
  `Gender` int(11) NOT NULL,
  `Type` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `skates`
--

INSERT INTO `skates` (`Id`, `Size`, `Gender`, `Type`) VALUES
(1, 0, 2, 1),
(2, 38, 0, 0),
(3, 39, 1, 1),
(4, 40, 2, 1),
(5, 41, 1, 1),
(6, 42, 0, 0),
(7, 42, 0, 0),
(8, 43, 1, 1),
(9, 44, 2, 1),
(10, 45, 1, 1),
(11, 46, 2, 0),
(12, 37, 1, 1),
(13, 38, 2, 1),
(14, 39, 1, 1),
(15, 40, 2, 0),
(16, 41, 1, 1),
(17, 42, 2, 1),
(18, 43, 1, 1),
(19, 44, 2, 0),
(20, 45, 1, 1),
(21, 46, 2, 1),
(22, 37, 1, 1),
(23, 38, 2, 0),
(24, 39, 1, 1),
(25, 40, 2, 1),
(26, 41, 1, 1),
(27, 42, 2, 0),
(28, 43, 1, 1),
(29, 44, 2, 1),
(30, 45, 1, 1),
(31, 46, 2, 0),
(32, 37, 1, 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user`
--

CREATE TABLE `user` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `PasswordInHash` longtext NOT NULL,
  `Role` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `user`
--

INSERT INTO `user` (`Id`, `Name`, `Email`, `PasswordInHash`, `Role`) VALUES
(1, 'Admin user', 'adminUser@gmail.com', '$2a$11$lC7ctNTDLloYpASLV0Hx8.NnnoUwPq.D/RomwDKynOYjvY1HJVGFy', 0),
(2, 'Normal user', 'normalUser@gmail.com', 'easy', 1),
(3, 'John Doe', 'johndoe@example.com', '$2a$11$lC7ctNTDLloYpASLV0Hx8.NnnoUwPq.D/RomwDKynOYjvY1HJVGFy', 1),
(4, 'Farkas Dániel', 'daniel.farkas@itiner.digital', '$2a$11$F63tjlpSv4cjzxT7ksOtEOY3B8SoX3qltjKSYfV1.Li2E9l4bJDK6', 0),
(8, 'Csabi', 'csabi.farkas@gmail.com', '$2a$11$CgJkRBkggM3thX8kJmQ/IOw9qCXHLk.OBYPrUzE.rn9lTv8nbQuXC', 1),
(9, 'Teszt Elek', 't.elek@mail.com', '$2a$11$tH0i2y5NEmdAnLIbcsp2M.Uu9OoaK3fCAf5guwag0iBubuFElhGhO', 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20250302131555_InitialCreate', '8.0.2'),
('20250305092246_AddSkateDBToDataSeeding', '8.0.2'),
('20250325084339_AddDatabaseContextConstructor', '8.0.2');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `events`
--
ALTER TABLE `events`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `rent`
--
ALTER TABLE `rent`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Rent_EventID` (`EventID`),
  ADD KEY `IX_Rent_SkateID` (`SkateID`),
  ADD KEY `IX_Rent_UserID` (`UserID`);

--
-- A tábla indexei `skates`
--
ALTER TABLE `skates`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `events`
--
ALTER TABLE `events`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT a táblához `rent`
--
ALTER TABLE `rent`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

--
-- AUTO_INCREMENT a táblához `skates`
--
ALTER TABLE `skates`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;

--
-- AUTO_INCREMENT a táblához `user`
--
ALTER TABLE `user`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `rent`
--
ALTER TABLE `rent`
  ADD CONSTRAINT `FK_Rent_Events_EventID` FOREIGN KEY (`EventID`) REFERENCES `events` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Rent_Skates_SkateID` FOREIGN KEY (`SkateID`) REFERENCES `skates` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Rent_User_UserID` FOREIGN KEY (`UserID`) REFERENCES `user` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

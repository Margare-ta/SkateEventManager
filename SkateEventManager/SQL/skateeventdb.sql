-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Ápr 10. 13:45
-- Kiszolgáló verziója: 10.4.27-MariaDB
-- PHP verzió: 8.2.0

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
(1, '2025-05-01 10:00:00.000000', '2025-05-03 18:00:00.000000', 'Spring Festival', 200, 50, 50),
(2, '2025-04-12 13:51:02.000000', '2025-04-19 13:51:02.000000', 'Skate ring at Liget', 80, 0, 40),
(5, '2025-04-02 00:00:00.000000', '2025-04-03 00:00:00.000000', 'Skate Ring at Liget', 120, 0, 22),
(6, '2025-06-09 00:00:00.000000', '2025-06-10 00:00:00.000000', 'Rolling Disco', 501, 0, 70),
(7, '2025-07-22 00:00:00.000000', '2025-07-22 00:00:00.000000', 'Skate under the starry sky', 105, 0, 6);

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
(9, 2, 1, 0, 94),
(10, 2, 2, 44, 102);

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
(94, 0, 2, 1),
(95, 38, 0, 0),
(96, 39, 1, 1),
(97, 40, 2, 1),
(98, 41, 1, 1),
(99, 42, 0, 0),
(100, 42, 0, 0),
(101, 43, 1, 1),
(102, 44, 2, 1),
(103, 45, 1, 1),
(104, 46, 2, 0),
(105, 37, 1, 1),
(106, 38, 2, 1),
(107, 39, 1, 1),
(108, 40, 2, 0),
(109, 41, 1, 1),
(110, 42, 2, 1),
(111, 43, 1, 1),
(112, 44, 2, 0),
(113, 45, 1, 1),
(114, 46, 2, 1),
(115, 37, 1, 1),
(116, 38, 2, 0),
(117, 39, 1, 1),
(118, 40, 2, 1),
(119, 41, 1, 1),
(120, 42, 2, 0),
(121, 43, 1, 1),
(122, 44, 2, 1),
(123, 45, 1, 1),
(124, 46, 2, 0),
(125, 37, 1, 1),
(126, 0, 2, 1),
(127, 38, 0, 0),
(128, 39, 1, 1),
(129, 40, 2, 1),
(130, 41, 1, 1),
(131, 42, 0, 0),
(132, 42, 0, 0),
(133, 43, 1, 1),
(134, 44, 2, 1),
(135, 45, 1, 1),
(136, 46, 2, 0),
(137, 37, 1, 1),
(138, 38, 2, 1),
(139, 39, 1, 1),
(140, 40, 2, 0),
(141, 41, 1, 1),
(142, 42, 2, 1),
(143, 43, 1, 1),
(144, 44, 2, 0),
(145, 45, 1, 1),
(146, 46, 2, 1),
(147, 37, 1, 1),
(148, 38, 2, 0),
(149, 39, 1, 1),
(150, 40, 2, 1),
(151, 41, 1, 1),
(152, 42, 2, 0),
(153, 43, 1, 1),
(154, 44, 2, 1),
(155, 45, 1, 1),
(156, 46, 2, 0),
(157, 37, 1, 1);

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
(2, 'John Doe', 'johndoe@example.com', '$2a$11$q8EfrCSUh0Bsro.ZtR8nneeyinZuisUal1PTMrpIa1hoPFipslJlm', 1),
(4, 'Kis Anna', 'annaKicsi@example.com', '$2a$11$5THrv7Z6HlRVkFwjskGADeouSlbN7mZjy/bW6wBicZgHcYnH0GKqW', 1),
(5, 'Admin', 'admin@example.com', '$2a$11$yvOIx17vXnzb7reHGT7m5OW4.YID3u2XZ22wXdNSwlYFyzUmMJ4E6', 0);

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
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT a táblához `rent`
--
ALTER TABLE `rent`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT a táblához `skates`
--
ALTER TABLE `skates`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=158;

--
-- AUTO_INCREMENT a táblához `user`
--
ALTER TABLE `user`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

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

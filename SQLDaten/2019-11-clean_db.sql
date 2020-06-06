-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 19. Okt 2019 um 12:10
-- Server-Version: 10.1.38-MariaDB
-- PHP-Version: 7.3.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `dsm`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `PatchDataCollection`
--

CREATE TABLE `PatchDataCollection` (
  `Id` int(11) NOT NULL,
  `Computer` longtext,
  `Patch` longtext,
  `Compliance` longtext,
  `FoundDate` datetime(6) NOT NULL,
  `FixDate` datetime(6) DEFAULT NULL,
  `CreatedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `users`
--

CREATE TABLE `Users` (
  `Id` int(11) NOT NULL,
  `Username` longtext,
  `PasswordHash` longblob,
  `PasswordSalt` longblob
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `Users`
--

INSERT INTO `Users` (`Id`, `Username`, `PasswordHash`, `PasswordSalt`) VALUES
(2, 'demo', 0x19f9312c167752497acc78b6790305dcf4f3bbc3db5c83cb276411d4951519908369b7444674ed1e0ef5a3d6c6e911d2d0a98b349d98c7c72f2da4284fdd42c7, 0x0523e00a7b5a5168211ea4ba66be4419746906c3383818e3d323ea038873c740e9c4a2db2bdb6b71c4ae1a51e633e1617f386c0bd67975f961ed01e2b7fed1830324e3a23985d8e6116d02f50dd9a651153f005e61076813c05be54efbb518c153dc2378c757649a1d3e04f0b3939aed285b8e57609bcbb5a8ec2fe850fb6e40);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20180914082811_AddedUserEntity', '2.1.11-servicing-32099'),
('20181007162406_touren_routen_gpxfiles', '2.1.11-servicing-32099'),
('20181007164613_tourRoutes_RandomAccessKey', '2.1.11-servicing-32099'),
('20181007164816_tourRoutes_korrektur', '2.1.11-servicing-32099'),
('20181007164940_tours_pflichtfelder_deklariert', '2.1.11-servicing-32099'),
('20181031144850_added_gear', '2.1.11-servicing-32099'),
('20181101131149_remove_correctedWeight_from_Gear', '2.1.11-servicing-32099'),
('20190528133015_patchdatacollection', '2.1.11-servicing-32099');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `PatchDataCollection`
--
ALTER TABLE `PatchDataCollection`
  ADD PRIMARY KEY (`Id`);

--
-- Indizes für die Tabelle `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`Id`);

--
-- Indizes für die Tabelle `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `PatchDataCollection`
--
ALTER TABLE `PatchDataCollection`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `Users`
--
ALTER TABLE `Users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

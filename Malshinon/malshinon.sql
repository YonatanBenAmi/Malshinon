-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 11, 2025 at 08:50 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `malshinon`
--

-- --------------------------------------------------------

--
-- Table structure for table `intelreports`
--

CREATE TABLE `intelreports` (
  `id` int(11) NOT NULL,
  `reporter_id` int(11) DEFAULT NULL,
  `target_id` int(11) DEFAULT NULL,
  `text` varchar(255) DEFAULT NULL,
  `timestamp` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `intelreports`
--

INSERT INTO `intelreports` (`id`, `reporter_id`, `target_id`, `text`, `timestamp`) VALUES
(2, 1, 1, 'Checking', '2025-06-10 17:35:27'),
(3, 2, 2, 'Checking', '2025-06-10 17:40:11'),
(7, 5, 5, 'report', '2025-06-11 09:26:07');

-- --------------------------------------------------------

--
-- Table structure for table `people`
--

CREATE TABLE `people` (
  `id` int(11) NOT NULL,
  `firstName` varchar(20) DEFAULT NULL,
  `lastName` varchar(20) DEFAULT NULL,
  `secret_code` varchar(15) DEFAULT NULL,
  `type` enum('reporter','target','both','potential_agent') DEFAULT NULL,
  `num_reports` int(11) DEFAULT 0,
  `num_mentions` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `people`
--

INSERT INTO `people` (`id`, `firstName`, `lastName`, `secret_code`, `type`, `num_reports`, `num_mentions`) VALUES
(1, 'Yonatan', 'Ben Ami', '123', 'reporter', 5, 56),
(2, 'Moshe', 'Lerre', '432', 'both', 2, 8),
(3, 'firstName', 'lastName', '111', '', 0, 0),
(5, 'avi', 'sgakom', '4444', 'both', 0, 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `intelreports`
--
ALTER TABLE `intelreports`
  ADD PRIMARY KEY (`id`),
  ADD KEY `reporter_id` (`reporter_id`),
  ADD KEY `target_id` (`target_id`);

--
-- Indexes for table `people`
--
ALTER TABLE `people`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `secret_code` (`secret_code`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `intelreports`
--
ALTER TABLE `intelreports`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `people`
--
ALTER TABLE `people`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `intelreports`
--
ALTER TABLE `intelreports`
  ADD CONSTRAINT `intelreports_ibfk_1` FOREIGN KEY (`reporter_id`) REFERENCES `people` (`id`),
  ADD CONSTRAINT `intelreports_ibfk_2` FOREIGN KEY (`target_id`) REFERENCES `people` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

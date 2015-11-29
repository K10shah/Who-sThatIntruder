-- phpMyAdmin SQL Dump
-- version 4.0.4.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Mar 10, 2015 at 04:14 AM
-- Server version: 5.5.32
-- PHP Version: 5.4.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `tmote`
--
CREATE DATABASE IF NOT EXISTS `tmote` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `tmote`;

-- --------------------------------------------------------

--
-- Table structure for table `intrusiondetails`
--

CREATE TABLE IF NOT EXISTS `intrusiondetails` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `moteid` int(10) NOT NULL,
  `timeofintrusion` varchar(50) NOT NULL,
  `location` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `userlogin`
--

CREATE TABLE IF NOT EXISTS `userlogin` (
  `UID` int(10) NOT NULL AUTO_INCREMENT,
  `username` varchar(25) NOT NULL,
  `password` varchar(10) NOT NULL,
  `Name` varchar(25) NOT NULL,
  `role` varchar(10) NOT NULL,
  PRIMARY KEY (`UID`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `userlogin`
--

INSERT INTO `userlogin` (`UID`, `username`, `password`, `Name`, `role`) VALUES
(1, 'admin', 'admin', 'admin', 'admin'),
(2, 'ketanshah', 'Ketan', 'ketan', 'user'),
(3, 'chandrabala', 'chandrabal', 'chandra', 'user'),
(5, 'abc', 'abc', 'abc', 'database-a');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

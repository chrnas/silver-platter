DROP DATABASE IF EXISTS SilverPlatterDB;
CREATE DATABASE SilverPlatterDB;
USE SilverPlatterDB;

CREATE TABLE Restaurants (
    RestaurantId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255),
    Description TEXT,
    Address VARCHAR(255)
) ENGINE=InnoDB;

CREATE TABLE MenuEntries (
    MenuEntryId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255),
    Description TEXT,
    RestaurantId INT NOT NULL,
    FOREIGN KEY (RestaurantId) REFERENCES Restaurants(RestaurantId) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE BookingTables (
    BookingTableId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255),
    Description TEXT,
    Places INT NOT NULL,
    RestaurantId INT NOT NULL,
    FOREIGN KEY (RestaurantId) REFERENCES Restaurants(RestaurantId) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE Users (
    UserId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255),
    Budget INT NOT NULL,
    PreferedRating INT NOT NULL
) ENGINE=InnoDB;

CREATE TABLE Allergies (
    AllergiesId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255),
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE RestaurantFavorites (
    RestaurantFavoritesId INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    RestaurantId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    FOREIGN KEY (RestaurantId) REFERENCES Restaurants(RestaurantId) ON DELETE CASCADE
) ENGINE=InnoDB;
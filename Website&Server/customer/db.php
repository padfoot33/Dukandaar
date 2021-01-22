<?php
$serverName = "localhost";
$dbUser = "id13064241_root"; //Copy your dbUser from Database manager
$dbPass = "ArcReacter@007"; //enter your pass from Database manager
$dbName = "id13064241_dukandar"; //Copy your dbName from Database manager
$conn = mysqli_connect($serverName, $dbUser, $dbPass, $dbName);

// Check connection
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}
?>
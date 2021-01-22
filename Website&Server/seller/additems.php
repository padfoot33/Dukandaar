<?php
if(count($_POST)>0) {
	require_once("db.php");
	$sql = "INSERT INTO items (shopid, city, items,name) VALUES ('".$_POST["shopid"]."','".$_POST["city"]."','".$_POST["items"]."','".$_POST["name"]."')";
	mysqli_query($conn,$sql);
	$current_id = mysqli_insert_id($conn);
	if(!empty($current_id)) {
		
	}
}
?>
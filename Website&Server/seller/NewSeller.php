<?php
if(count($_POST)>0) {
	require_once("db.php");
	$sql = "INSERT INTO seller (name, state, city, shopname,address) VALUES ('".$_POST["name"]."','".$_POST["state"]."','".$_POST["city"]."','".$_POST["shopname"]."','".$_POST["address"]."')";
	mysqli_query($conn,$sql);
	$current_id = mysqli_insert_id($conn);
	if(!empty($current_id)) {
		
	}
}
?>
<?php
if(count($_POST)>0) {
	require_once("db.php");
	$sql = "select * from `seller` where `city`='".$_POST["city"]."'";
	$result = $conn->query($sql);
	
	while($row = $result->fetch_assoc()){
	    echo $row['shop_id'].";".$row['name'].";".$row['shopname'].";".$row['address'].";".$row['state']."/";
	}
}
?>
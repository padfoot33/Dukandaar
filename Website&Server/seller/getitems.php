<?php
if(count($_POST)>0) {
	require_once("db.php");
	$sql = "select * from `items` where `shopid`='".$_POST["shopid"]."'";
	$result = $conn->query($sql);
	
	while($row = $result->fetch_assoc()){
	    echo $row['items'].";".$row['name']."/";
	}
}
?>
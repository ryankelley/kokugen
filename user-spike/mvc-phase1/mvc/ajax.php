<?
if(isset($_REQUEST["id"])){
	echo "[{ id : 1, name : 'item 1' }]";
}else{
	echo "[{ id : 1, name : 'item 1' }, { id : 2, name : 'item 2' }]";
}
?>
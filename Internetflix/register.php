<?php
include ('userStorage.php');

$uStorage=new userStorage();

$data=[];
$errors=[];

function validate($post,&$data,&$errors) {

    if (!isset($post['username']) || trim($post['username']==='')) {
        $errors['username']="Nem adtál meg felhasználónevet!";
    } else {
        $data['username']=$post['username'];
    }

    if (!isset($post['email']) || trim($post['email']==='')) {
        $errors['email']="Nem adtál meg email címet!";
    } else if (!filter_var($post['email'],FILTER_VALIDATE_EMAIL)) {
        $errors['email']="Rosszul adtad meg az email címet!";
    } else {
        $data['email']=$post['email'];
    }

    if (!isset($post['password']) || trim($post['password']==='')) {
        $errors['password']="Nem adtad meg a jelszót!";
    } else if ($post['password']!==$post['passwordU']) {
        $errors['password']="A beírt jelszavak nem egyeznek!";
    } else {
        $data['password']=password_hash($post['password'],PASSWORD_DEFAULT);
    }

    return count($errors)===0;
}

function user_exists($userStorage,$username) {
    $user = $userStorage->findOne(['username' => $username]);
    return !is_null($user);
}

if (count($_POST)!==0) {
    if (validate($_POST,$data,$errors)) {
        if (user_exists($uStorage,$data['username'])) {
            $errors['username'] = "A felhasználónév foglalt";
        } else {
            $user = [
                'username' => $data['username'],
                'password' => $data['password'],
                'email' => $data['email'],
                'watched' => []
            ];
            $uStorage->add($user);
            header("Location: login.php");
            exit();
        }
    }
}

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Internetflix regisztráció</title>
    <link rel="stylesheet" href="index.css">
</head>
<body>
    <h1>INTERNETFLIX - regisztráció</h1>
    <form action="" method="post" class="form" novalidate>
        Felhasználónév: <input type="text" name="username" id="" value="<?=$_POST['username'] ?? ''?>"><span><?=$errors['username'] ?? ''?></span><br>
        Email cím: <input type="email" name="email" id="" value="<?=$_POST['email'] ?? ''?>"><span><?=$errors['email'] ?? ''?></span><br>
        Jelszó: <input type="text" name="password" id="" value="<?=$_POST['password'] ?? ''?>"><span><?=$errors['password'] ?? ''?></span><br>
        Jelszó újból: <input type="text" name="passwordU" id="" value="<?=$_POST['passwordU'] ?? ''?>"><span><?=$errors['passwordU'] ?? ''?></span><br>
        <button>Regisztrálás</button>
    </form>
</body>
</html>
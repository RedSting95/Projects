<?php
include ('userStorage.php');

session_start();

$uStorage=new userStorage();

$data=[];
$errors=[];

function validate($post,&$data,&$errors) {

    if (!isset($post['username']) || trim($post['username']==='')) {
        $errors['username']="Nem adtál meg felhasználónevet!";
    } else {
        $data['username']=$post['username'];
    }

    if (!isset($post['password']) || trim($post['password']==='')) {
        $errors['password']="Nem adtad meg a jelszót!";
    } else {
        $data['password']=$post['password'];
    }

    return count($errors)===0;
}

function check_user($userStorage,$username,$password) {
    $users=$userStorage->findMany(function ($user) use ($username,$password) {
        return $user['username'] === $username && password_verify($password,$user['password']);
    });
    return count($users) === 1 ? array_shift($users) : NULL;
}

function login($user) {
    $_SESSION['user']=$user;
}

if (count($_POST)!==0) {
    if (validate($_POST,$data,$errors)) {
        $logged_in_user = check_user($uStorage,$data['username'],$data['password']);
        if (!$logged_in_user) {
            $errors['username'] = "Helytelen felhasználónév, vagy jelszó!";
        } else {
            login($logged_in_user);
            header('location: Fooldal.php');
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
    <title>Internetflix bejelentkezés</title>
    <link rel="stylesheet" href="index.css">
</head>
<body>
    <h1>INTERNETFLIX - bejelentkezés</h1>
    <form action="" method="post" class="form" novalidate>
        Felhasználónév: <input type="text" name="username" id="" value="<?=$_POST['username'] ?? ''?>"><span><?=$errors['username'] ?? ''?></span><br>
        Jelszó: <input type="text" name="password" id="" value="<?=$_POST['password'] ?? ''?>"><span><?=$errors['password'] ?? ''?></span><br>
        <button>Bejelentkezés</button>
    </form>
</body>
</html>
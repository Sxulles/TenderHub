import LoginForm from "../components/LoginForm";

const LoginPage = ({usernameSetter}) => {
    return (
        <LoginForm usernameSetter={usernameSetter}/>
    );
}
 
export default LoginPage;
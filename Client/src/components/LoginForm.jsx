import { useEffect, useState } from 'react';
import { Form, Button, Container, Row, Col } from 'react-bootstrap';
import Cookies from 'js-cookie';
import googleIcon from "../assets/svg/google.svg"
import { useNavigate } from 'react-router-dom';

const LoginForm = () => {

  const navigate = useNavigate();

  const [successfulLogin, setSuccessfulLogin] = useState(false);

  const [formData, setFormData] = useState({
    email: '',
    password: '',
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  useEffect(() => {
    if(successfulLogin){
      navigate("/advertisements")
    }
  }, [successfulLogin])

  const handleSubmit = async (e) => {
    e.preventDefault();
    const result = await fetchApi()

    if(result.success){
      console.log(result)

      Cookies.set('Authorization', result.token, { secure: true, sameSite: 'strict' });
      Cookies.set('Username', result.username, {secure: true, sameSite: "strict"})

      setSuccessfulLogin(prevState => prevState = true)

      console.log("JWT Token saved")
    }
    else {
      setSuccessfulLogin(prevState => prevState = false)
    }
    console.log(result)
  };

  const fetchApi = async () => {
    try {
      const response = await fetch('https://localhost:7026/Auth/Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
      });

      if (response.ok) {
        return await response.json();
      } else {
        //Api error
      }
    } catch (error) {
      //Fetch error
    }
  };

  
  return (
    <Container className='pt-5'>
      <Row className="justify-content-center">
        <Col md={6}>
          <Container className='mb-5 p-2'>
            <Row>
              <Col md={6} className='text-center'><Button variant='secondary' size='lg' className='px-5'>Login</Button></Col>
              <Col md={6} className='text-center'><Button variant='outline-primary' size='lg' className='px-5'>Register</Button></Col>
            </Row>
            <Row className='text-center mt-4'>
              <p>Sign in with:</p>
              <img className='my-4' width={20} height={20} src={googleIcon} alt="" />
              <p>or:</p>
            </Row>
          </Container>
          <Form onSubmit={handleSubmit}>
            <Form.Group className='my-4' controlId="email">
              <Form.Label>Email</Form.Label>
              <Form.Control
                type="email"
                name="email"
                value={formData.email}
                placeholder='Email or username'
                onChange={handleInputChange}
                required
              />
            </Form.Group>

            <Form.Group className='my-4' controlId="password">
              <Form.Label>Password</Form.Label>
              <Form.Control
                type="password"
                name="password"
                placeholder='Password'
                value={formData.password}
                minLength={6}
                onChange={handleInputChange}
                required
              />
            </Form.Group>

            <Container fluid>
              <Row className='text-center'>
                <a href="">Forgot password?</a>
              </Row>
            </Container>

            <div className="d-grid gap-2 my-4">
              <Button variant="primary" type="submit" className='shadow'>
                Sign in
              </Button>
            </div>
            
          </Form>
        </Col>
      </Row>
    </Container>
  );
};

export default LoginForm;
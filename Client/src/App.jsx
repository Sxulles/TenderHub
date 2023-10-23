
import NavMenu from './components/NavMenu';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import AuthorizeRoute from './components/auth/AuthorizeRoute';

function App() {

  return (
    <Router>
      <div>
        <NavMenu />
        <Routes>
          {AppRoutes.map((route, index) => (
            <Route
              key={index}
              path={route.path}
              element={<AuthorizeRoute element={route.element} requireAuth={route.requireAuth}/>}
            />
          ))}
        </Routes>
      </div>
    </Router>
  )
}

export default App;
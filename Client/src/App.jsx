import React, { useState } from 'react';

import LoginPage from './pages/LoginPage';
import AdvertisementPage from './pages/AdvertisementPage';
import NavMenu from './components/NavMenu';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

function App() {

  const [user, setUser] = useState("");

  const usernameSetter = (username) => {
    setUser(prev => prev = username)
  }

  return (
    <Router>
      <div>
        <NavMenu />
        <Routes>
          <Route path="/login" element={<LoginPage usernameSetter={usernameSetter} />} />
          <Route path="/advertisements" element={<AdvertisementPage user={user} />} />
        </Routes>
      </div>
    </Router>
  )
}

export default App;
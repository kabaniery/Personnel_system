import './App.css'
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom'
import Navbar from './components/Navbar';
import SubdivisionByDateForm from './pages/SubdivisionByDateForm';
import HomePage from './pages/Home';
import NewEmployee from './pages/NewEmployee';
import EmployeeByDate from './pages/EmployeeBySubdivisionAndDate';
import AuthForm from './pages/Auth/Auth';

const App: React.FC = () => {
  return (
    <Router>
      <div className="App">
        <Navbar />

        {/* Настройка маршрутов */}
        <Routes>
          <Route path="/subdivision/bydate" element={<SubdivisionByDateForm />} />
          <Route path="/employee/create" element={<NewEmployee />} />
          <Route path="/" element={<HomePage />} />
          <Route path='/employee/bydate' element={<EmployeeByDate />} />
          <Route path='/auth' element={<AuthForm/ >} />
        </Routes>
      </div>
    </Router>
  );
}

export default App

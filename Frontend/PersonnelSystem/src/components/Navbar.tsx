import { useState } from "react"
import { Link } from "react-router-dom";
import './Navbar.css';

const Navbar: React.FC = () => {
    const [isOpen, setIsOpen] = useState(false);

    const toggleMenu = () => {
        setIsOpen(!isOpen);
    };

    return (
        <div className="navbar">
      <button className="menu-toggle" onClick={toggleMenu}>☰</button>
      {isOpen && (
        <div className="context-menu">
          <ul>
            <li>
              <Link to="/employee/create" onClick={toggleMenu}>Добавить сотрудника</Link>
            </li>
            <li>
              <Link to="/subdivision/bydate" onClick={toggleMenu}>Вывести список подразделений</Link>
            </li>
            <li>
              <Link to="/employee/bydate" onClick={toggleMenu}>Вывести список сотрудников в подразделении</Link>
            </li>
          </ul>
        </div>
      )}
    </div>
  );
};

export default Navbar;
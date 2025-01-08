import { StrictMode, useEffect, useState } from 'react'
import App from '../App.tsx'

const notAuth = (): boolean => {
  const storedPassword = localStorage.getItem('AuthPass');
  return !storedPassword;
}

const Main: React.FC = () => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(!notAuth());
  const [loading, setLoading] = useState<boolean>(true);
  
  useEffect(() => {
    if (!isAuthenticated) {
      const promptForPassword = () => {
        const userPassword = prompt('Please enter the password to continue:');
        if (userPassword)
        {
          localStorage.setItem('AuthPass', userPassword);
          setIsAuthenticated(true);
        }
        else {
          alert('Please, enter password to continue.');
          promptForPassword();
        }
      };
      promptForPassword();
    } else {
      setLoading(false);
    }
  }, [isAuthenticated]);

  if (loading) {
    return <div>Loading...</div>;
  }

  return <StrictMode><App /></StrictMode>;
};

export default Main;
import React, { useState } from 'react';
import axios from 'axios';

const PasswordForm = () => {
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await axios.get('http://localhost:5000/api/react/checkme', {
        headers: {
          Authorization: password,
        },
      });

      if (response.status === 200) {
        localStorage.setItem('AuthPass', password);
        setErrorMessage(''); 
        alert('Пароль успешно сохранен!');
      }
    } catch (error) {
      setErrorMessage('Пароль неверный');
    }
  };

  return (
    <div style={{ maxWidth: '400px', margin: '0 auto', textAlign: 'center' }}>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="password">Пароль:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            style={{ width: '100%', padding: '8px', margin: '10px 0' }}
          />
        </div>
        <button type="submit" style={{ padding: '8px 16px' }}>Отправить</button>
      </form>
      {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
    </div>
  );
};

export default PasswordForm;

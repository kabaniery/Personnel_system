import React, { useState } from 'react';
import axios from 'axios';
import Subdivision from '../types/subdivision';
import { useNavigate } from 'react-router-dom';
import './Forms.css';

const SubdivisionByDateForm: React.FC = () => {
  const [date, setDate] = useState('');
  const [result, setResult] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault(); 
    setLoading(true);
    setError(null);

    await axios.get('http://localhost:5000/api/react/subdivision/bydate', { 
      params: {
          'date': date
      }, 
      headers: {
          'Content-Type': 'application/json',
          'Authorization': localStorage.getItem("AuthPass"),
      }
    }).then(response => {
      if (response.status == 200) {
        let rawData = "";
        const subdivisions = response.data;
        subdivisions.forEach((subdiv: Subdivision) => {
            rawData += subdiv.name + "; ";
        }); 
        setResult(rawData);
      }
    }).catch(error => {
      if (error.response) {
        if (error.response.status === 401) {
            navigate('/auth');
        }
    }
    });
    setLoading(false);
  };

  return (
    <div>
      <h1>Поиск подразделений по дате</h1>
      <form onSubmit={handleSubmit} className='form-container'>
        <label>
          Введите дату:
          <input
            type="date"
            value={date}
            onChange={(e) => setDate(e.target.value)}
          />
        </label>
        <button type="submit" disabled={loading}>
          {loading ? 'Загрузка...' : 'Найти'}
        </button>
      </form>

      {error && <div style={{ color: 'red', marginTop: '10px' }}>{error}</div>}

      {result && (
        <div
          style={{
            marginTop: '20px',
            padding: '10px',
            border: '1px solid #ccc',
            borderRadius: '5px',
          }}
        >
          <strong>Результат поиска:</strong> {result}
        </div>
      )}
    </div>
  );
};

export default SubdivisionByDateForm;

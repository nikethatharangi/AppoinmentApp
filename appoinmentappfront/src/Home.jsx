
import Delete from './Delete'
import Edit from './Edit'
import New from './New'         


export default function Home(props) {
  return (
    <div>
      <h2>Manage Your Appointments / Dates very easy</h2>
      <p>This powerful web applicaiton helps you to manage your dates very easy.</p>
      <div className="add-btn row items-center content-center">
        <div className="btn add">+</div>
      </div>
      <div className="notifications spacer-20">Ths is Text.</div>
        <Delete/>
        <Edit/>
        <New/>
     
    </div>
  );
}
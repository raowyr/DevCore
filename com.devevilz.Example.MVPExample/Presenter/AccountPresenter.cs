using com.devevil.MVP.Presenter;
using com.devevilz.Example.MVPExample.Model;
using com.devevilz.Example.MVPExample.Services;
using com.devevilz.Example.MVPExample.View;
using System;

namespace com.devevilz.Example.MVPExample.Presenter
{
    public class AccountPresenter : Presenter<IAccountView, IAccountPresenter>, IAccountPresenter
    {
        private AccountService accService = null;

        //Attenzione, gestione con DependencyInjection linizializzazione del service nel presenter
        public AccountPresenter(IAccountView _view) : base(_view) 
        {
            accService = new AccountService();
        }

        public void handleLogin()
        {
            try
            {
                //Gestione della logica per l'operazione di Login
                if (!String.IsNullOrEmpty(View.Username) && !String.IsNullOrEmpty(View.Password))
                {
                    Account acc = accService.getAccount(View.Username, View.Password);
                    if (acc != null)
                    {
                        //Account trovato, verifichiamo che sia abilitato o meno
                        if (acc.IsEnabled)
                        {
                            //ok, utente abilitato, possiamo settare una variabile di stato per
                            //ricordare l'utente loggato
                            View.LogState = true;
                        }
                        else
                        {
                            //utente non ailitato...
                            View.showMessage("Non sei abilitato all'accesso");
                        }
                    }
                    else
                    {
                        //Account non trovato...
                        View.showMessage("Username e/o password non validi");
                    }
                }
                else
                {
                    //Richiama metodi del model o delle classi service per verificare la correttezza
                    //dei dati di login
                    View.showMessage("Compila correttamente tutti i campi");
                }
            }
            catch (Exception ex)
            {
                handleException(ex, false);
            }
        }

        public void handleCreateAccount()
        {
            throw new NotImplementedException();
        }

        public void handleModfyAccount()
        {
            throw new NotImplementedException();
        }

        public override void initView()
        {
            //throw new NotImplementedException();
        }

        public override void handleException(Exception ex, bool rethrow)
        {
            View.showMessage(ex.Message);
        }
    }
}

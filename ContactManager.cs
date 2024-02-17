using ConsoleTables;
using Humanizer;

namespace BasicContactList
{
    internal sealed class ContactManager : IContactManager
    {
        public static List<Contact> Contacts = new();
        //implementing my interface method(AddContact)
        public void AddContact(string name, string phoneNumber, string? email, ContactType contactType)
        {
            try
            {
                int id = Contacts.Count > 0 ? Contacts.Count + 1 : 1;
                var isContactExist = IsContactExist(phoneNumber);
                if (isContactExist)
                {
                    Console.WriteLine("Contact already exist!");
                    return;
                }

                    var contact = new Contact
                {
                    Id = id,
                    Name = name,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    ContactType = contactType,
                    CreatedAt = DateTime.Now
                };

                    Contacts.Add(contact);
                     Console.WriteLine("Contact added successfully.");

                using (StreamWriter contactList = new ("contactList.txt", true))
                {
                    contactList.WriteLine($"Name:  {name}\n" + $"Phone Number: {phoneNumber}\n" + $"Email: {email}\n" + $"Contact Type: {contactType}\n" + $"Created at: {DateTime.Now}\n");
                    
                } 
                
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding this  contact: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("");
            }
        }

        //implementing my interface method(DeleteContact)
        public void DeleteContact(string phoneNumber)
        {
           try
            {
                
                var contact = FindContact(phoneNumber);

              

                if (contact is null)
                {
                    Console.WriteLine("Unable to delete contact as it does not exist!");
                    return;
                }
                else
                {
                    Console.WriteLine("contact deleted successfully");
                }

                Contacts.Remove(contact);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting a contact: {ex.Message}");
            }
        }

        //implementing my interface method(FindContact)
        public Contact? FindContact(string phoneNumber)
        {
            try
            {
                return Contacts.Find(c => c.PhoneNumber == phoneNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while finding a contact: {ex.Message}");
                return null; 
            }
        }

        //implementing my interface method(GetContact)
        public void GetContact(string phoneNumber)
        {
          
           try
           {
          
                var contact = FindContact(phoneNumber);
                
                if (contact is null)
                {
                    Console.WriteLine($"Contact with {phoneNumber} not found");
                }
                else
                {
                    Print(contact);
                }
          
           }
           catch (Exception ex)
           {
            Console.WriteLine("an error occur while finding a contact!!:" + ex.Message);
           }
        }

        //implementing my interface method(GetAllContact)
        public void GetAllContacts()
        {
         try
         {
                   int contactCount = Contacts.Count;

                Console.WriteLine("You have " + "contact".ToQuantity(contactCount));

                if (contactCount == 0)
                {
                    Console.WriteLine("There is no contact added yet.");
                    return;
                }

                var table = new ConsoleTable("Id", "Name", "Phone Number", "Email", "Contact Type", "Date Created");

                foreach (var contact in Contacts)
                {
                    table.AddRow(contact.Id, contact.Name, contact.PhoneNumber, contact.Email, ((ContactType)contact.ContactType).Humanize(), contact.CreatedAt.Humanize());
                }

                table.Write(Format.Alternative);

              

                using (StreamReader readAllContacts = new ("ContactList.txt"))
                {
                    string? contactList = readAllContacts.ReadLine();
                    while (contactList is not null)
                    {
                        Console.WriteLine(contactList);
                        contactList = readAllContacts.ReadLine();
                    }
                }
         }
         catch(Exception ex)
         {
            Console.WriteLine("an error occur while getting all contact" + ex.Message);

         }
        }
        //implementing my interface method( Updateontact )
        public void UpdateContact(string phoneNumber, string name, string email)
        {
           try
           {
                 var contact = FindContact(phoneNumber);

                if (contact is null)
                {
                    Console.WriteLine("Contact does not exist!");
                    return;
                }

                contact.Name = name;
                contact.Email = email;
                Console.WriteLine("Contact updated successfully.");

             }
             catch(Exception ex)
             {
                Console.WriteLine("an error occur when updating contact:" + ex.Message);
                
             }
          
        }

        private void Print(Contact contact)
        {
            Console.WriteLine($"Name: {contact!.Name}\nPhone Number: {contact!.PhoneNumber}\nEmail: {contact!.Email}");
        }

        private bool IsContactExist(string phoneNumber)
        {
            return Contacts.Any(c => c.PhoneNumber == phoneNumber);
        }
    }
}






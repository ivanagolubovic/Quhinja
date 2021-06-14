<template>
  <div class="q-pa-md row items-start q-gutter-md">
    <div class="cards">
      <div v-if="!this.admin" class="hidden1">
        <q-card>
          <div class="firstCard">
            <q-img src="../../public/add.svg" />
          </div>
        </q-card>
      </div>
      <div v-else class="row items-center addCard">
        <div class="col-12 q-pa-xl">
          <div
            class="col-12 q-pa-md"
            style="display: flex; flex-direction: row; justify-content: center"
          >
            <div class="firstCard" @click="handleNewDishClick">
              <q-img src="../../public/add.svg" />
            </div>
          </div>
          <div class="col-4 q-tm-md">
            <label class="text-h6 name">Dodajte novo jelo</label>
          </div>

          <div class="Cup q-pt-xl">
            <q-img src="../../public/dodajNovoJelo.png" />
          </div>
        </div>
      </div>
      <q-card
        v-for="(dish, index) in dishesForView.slice(
          (currentPage - 1) * dishesPerPage,
          currentPage * dishesPerPage
        )"
        :key="index"
        class="my-card"
      >
        <div class="col-4">
          <q-img :src="dish.image" style="height: 200px; width: 100%" />

          <div class="q-pa-md">
            <div class="col text-h6 name ellipsis">
              {{ dish.name }}
            </div>
          </div>
          <div class="row q-pa-md">
            <q-rating
              v-model="dish.averageRating"
              size="2em"
              :max="5"
              color="yellow"
              readonly
            />
            <div class="q-mx-sm"></div>
            <div class="text-h6 text-yellow justify-content:flex-end">
              {{ dish.averageRating }}
            </div>
          </div>
        </div>
        <div class="col-6">
          <div
            class="q-px-md text-subtitle1 name"
            style="
              text-overflow: ellipsis;
              width: 232px;
              height: 60px;
              overflow: hidden;
              white-space: nowrap;
              text-overflow: ellipsis;
            "
          >
            {{ dish.description }}
          </div>

          <div
            style="
            position: absolute;
            right: 10px;
            bottom: 1px
            margin: 0 8.5px 0 0;
            font-family: Montserrat;
            font-size: 16px;
            font-weight: bold;
            font-stretch: normal;
            font-style: normal;
            color: #5c5840;"
            @click="handleClick(dish.id)"
            flat
          >
            See more >>
          </div>
        </div>
      </q-card>
    </div>
    <q-dialog v-model="visibleDishForm" persistent @hide="handleHideDishDialog">
      <q-card class="q-py-sm full-width text-accent">
        <q-card-section
          class="q-ml-sm row full-width justify-between items-center"
        >
          <div class="text-h5 text-brown-5">Dodavanje novog jela</div>
          <q-btn
            icon="close"
            class="text-brown-5"
            flat
            round
            dense
            @click="handleHideDishDialog"
          />
        </q-card-section>
        <q-separator></q-separator>
        <q-card-section class="q-pt-none">
          <q-form
            ref="form"
            class="full-width column q-gutter-y-sm"
            @submit="handleSubmitDishForm"
          >
            <div
              class="row justify-around q-pt-md text-h6 text-brown-5"
              style="height: 45px"
            >
              <div class="col-6">Ime jela</div>
              <div class="col-6">
                <div class="row justify-between">
                  <q-item-label>Ubaci sliku</q-item-label>
                </div>
              </div>
            </div>
            <div class="row justify-around">
              <div class="col-6 q-pr-sm">
                <q-input filled v-model="text" label="Npr. Pizza" />
              </div>
              <div class="col-6">
                <q-file
                  filled
                  bottom-slots
                  v-model="model"
                  label="Upload image"
                  counter
                >
                  <template v-slot:prepend>
                    <q-icon name="cloud_upload" @click.stop />
                  </template>
                  <template v-slot:append>
                    <q-icon
                      name="close"
                      @click.stop="model = null"
                      class="cursor-pointer"
                    />
                  </template>
                </q-file>
              </div>
            </div>
            <div class="row text-h6 text-brown-5">
              <div class="col-6 column justify-around text-h6 text-brown-5">
                <q-item-label>Dozvoli ocenjivanje jela</q-item-label>
                <q-option-group
                  v-model="group"
                  :options="options"
                  color="primary"
                  inline
                />
              </div>
              <div class="col-6 column justify-around text-h6 text-brown-5">
                <q-item-label>Da li je jelo ljuto?</q-item-label>
                <q-option-group
                  v-model="group1"
                  :options="options1"
                  color="primary"
                  inline
                />
              </div>
            </div>

            <q-item-label class="text-brown-5 text-h6">Sastojci</q-item-label>

            <q-item-label class="text-brown-5 text-h6">Opis jela</q-item-label>
            <q-input
              style="width: 100%"
              filled
              v-model="text"
              label="Napisi nesto o jelu..."
            />
          </q-form>
        </q-card-section>
      </q-card>
    </q-dialog>
  </div>
</template>
<script>
import { baseUrl } from "../services/apiConfig";
export default {
  data() {
    return {
      group: "Da",
      options: [
        {
          label: "Da",
          value: "1",
        },
        {
          label: "Ne",
          value: "2",
        },
      ],
      group1: "Da",
      options1: [
        {
          label: "Da",
          value: "3",
        },
        {
          label: "Ne",
          value: "4",
        },
      ],
      ratingModel: 0,
      visibleDishForm: false,
      dishes: [],
      sortingOptions: [],
      sortingOption: "",
      dishesForView: [],
      search: "",
      dishesPerPage: 8,
      usersRating: null,
      currentPage: 1,
      admin: false,
      userData: {},
      sortBool: false,
    };
  },
  computed: {
    numOfDishes() {
      return this.dishesForView.length;
    },
    numOfPages() {
      if (this.numOfDishes % this.dishesPerPage == 0)
        return this.numOfDishes / this.dishesPerPage;
      else {
        return this.numOfDishes / this.dishesPerPage + 1;
      }
    },
  },
  watch: {
    search: function () {
      if (this.search == "") {
        this.dishesForView = this.dishes;
      } else {
        this.dishesForView = this.dishesForView.filter((dish) => {
          return dish.name.toLowerCase().startsWith(this.search.toLowerCase());
        });
      }
    },
  },
  filters: {
    ParseFloat(number) {
      let newValue = parseFloat(number).toFixed(2);
      return newValue;
    },
  },
  methods: {
    handleSubmitDishForm() {
      console.log(this.formData);

      this.$store
        .dispatch("auth/register", {
          ...this.formData,
          dateOfBirth: moment(
            this.formData.birthDate,
            this.dateFormatString
          ).format(),
          dateOfEmployment: moment(
            this.formData.firstDayAtWork,
            this.dateFormatString
          ).format(),
        })
        .then((response) => {
          this.$q.notify({
            position: "top",
            message: "Uspesno ste registrovali korisnika",
            color: "brown",
          });
          this.visibleDishForm = false;
          this.formData = {
            email: "",
            username: "",
            name: "",
            surname: "",
            position: "",
            birthDate: null,
            firstDayAtWork: null,
            gender: null,
            admin: false,
          };
          this.getData();
        })
        .catch((response) =>
          this.$q.notify({
            position: "top",
            message: response.data,
            type: "negative",
          })
        );
    },

    handleNewDishClick() {
      this.visibleDishForm = true;
    },
    handleHideDishDialog() {
      this.visibleDishForm = false;
    },
    handleClick(id) {
      this.$router.push("dish/" + id);
    },
    handleClickPlus() {
      this.$router.push("addDish");
    },
    sortDishes(option) {
      this.sortBool = true;
      this.dishesForView = [];
      this.dishes.forEach((element) => {
        if (element.dishType == option) this.dishesForView.push(element);
      });
    },
    getData() {
      this.$store
        .dispatch("apiRequest/getApiRequest", { url: "Dish" })
        .then((res) => {
          this.dishes = res;

          this.dishes.forEach((element) => {
            element.image = "data:image/png;base64," + element.image;
          });
          this.dishesForView = this.dishes;
        });
    },
    getDishTypes() {
      this.$store
        .dispatch("apiRequest/getApiRequest", { url: `/dish/dishTypes` })
        .then((res) => {
          this.sortingOptions = res;
        });
    },

    getUsersData() {
      this.$store
        .dispatch("apiRequest/getApiRequest", { url: "user/0" })
        .then((res) => {
          this.userData = res;
          this.check();
        });
    },
    check() {
      this.userData.roles.forEach((el) => {
        if (el == "admin") return (this.admin = true);
      });
    },
  },
  created() {
    this.getData();
    this.getDishTypes();
    this.getUsersData();
  },
};
</script>

<style scoped>
.cards {
  justify-content: center;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  position: relative;
}

.my-card,
.addCard {
  width: 260px;
  height: 505px;
  margin: 59px 20px 31px;
  padding: 0 0 29px;
  background-color: #fbfbfb;
  display: flex;
  flex-direction: column;
  align-content: center;
}

.name {
  color: #5c5840;
}
.hidden1 {
  display: none;
}
.firstCard {
  width: 29.3px;
  height: 29.3px;
  background-color: #fbfbfb;
  display: flex;
}
.cup {
  width: 185px;
  height: 230px;
}
.cardClass {
  display: flex;
  flex-direction: column;
  justify-items: stretch;
}
.picture {
  width: 100%;
  height: 280px;
}
.plus {
  width: 19.5px;
  height: 19.5px;
}
</style>